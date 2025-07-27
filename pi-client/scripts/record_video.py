# record_video.py
# This script preview a video using the Picamera2 library.
from picamera2 import Picamera2
from picamera2.encoders import H264Encoder
import time

picam2 = Picamera2()
video_config = picam2.create_video_configuration()
picam2.configure(video_config)
encoder = H264Encoder()
print("Starting video recording...")

picam2.start_recording(encoder, output="video_001.h264")
time.sleep(10)  # Record for 10 seconds
picam2.stop_recording()
print("Video recording completed and saved as video_001.h264")
picam2.close()
