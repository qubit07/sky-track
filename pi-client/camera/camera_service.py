from picamera2 import Picamera2
from picamera2.encoders import H264Encoder
from sensors.motion_detection import MotionDetection
from camera.camera_state import CameraState
import time, logging
from pathlib import Path

logger = logging.getLogger(__name__) 

class CameraService:
    def __init__(self, video_dir="videos", video_duration=10, video_max_duration=30):
        self.video_dir = Path(video_dir)
        self.video_dir.mkdir(parents=True, exist_ok=True)
        self.video_duration = video_duration
        self.video_max_duration = video_max_duration
        self.motion_detected_at = None
        self.running = False
        self.state = CameraState.IDLE
        self.motion_detection = MotionDetection()
        self.picam2 = Picamera2()

    def _initialize_camera(self):
        config = self.picam2.create_video_configuration()
        self.picam2.configure(config)
        self.picam2.start()
        time.sleep(1)  # Allow camera to warm up
        self.running = True
        self.state = CameraState.IDLE
        logger.info("Camera started.")

    def start(self):
        if self.running:
            logger.info("Camera is already running.")
            return
        
        self._initialize_camera()
        while self.running:
            try:
                frame = self.picam2.capture_array()
                if self.motion_detection.detect(frame):
                    self._handle_motion_detected()
                elif self.state == CameraState.RECORDING:
                    self._stop_recording()
            except Exception as e:
                logger.error(f"An error occurred: {e}")
                self._handle_error()
                break
        logger.info("Camera service stopped.")      

    def _handle_motion_detected(self):
        timestamp = int(time.time())
        if self.state == CameraState.IDLE:
            logger.info("Motion detected, capturing image...")
            self.motion_detected_at = timestamp
            self._capture_image(timestamp)
            self._record_video(timestamp)

        elif self.state == CameraState.RECORDING:
            elapsed = time.time() - self.motion_detected_at
            if elapsed >= self.video_max_duration:
                logger.info("Max recording duration reached, stopping recording.")
                self._stop_recording()
            else:
                logger.info("Extending recording duration due to continued motion.")
                time.sleep(self.video_duration)


    def _handle_error(self):
        logger.info("An error occurred. Stopping camera service.")
        self.state = CameraState.ERROR
        self.picam2.stop()
        self.running = False
        time.sleep(10)  # Allow some time for cleanup
        self.start()  # Restart the camera service

    def _capture_image(self, timestamp):
        filename  = self.video_dir / f"capture_{timestamp}.jpg"
        self.picam2.capture_file(str(filename))
        logger.info(f"Image captured and saved as {filename }")

    def _record_video(self, timestamp):
        filename  = self.video_dir / f"video_{timestamp}.h264"
        logger.info(f"Starting video recording: {filename }")
        encoder = H264Encoder()
        self.picam2.start_recording(encoder, output=str(filename))
        self.state = CameraState.RECORDING
        time.sleep(self.video_duration)

    def _stop_recording(self):
        logger.info("Stopping video recording.")
        self.picam2.stop_recording()
        self.motion_detected_at = None
        self.state = CameraState.IDLE
        self._initialize_camera()

    def stop(self):
        if not self.running:
            logger.info("Camera is not running.")
            return    
        self.picam2.stop()
        self.running = False
        logger.info("Camera stopped.")