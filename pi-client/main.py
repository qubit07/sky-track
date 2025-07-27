# main.py

import config
from camera.camera_service import CameraService

def main():
    camera_service = CameraService(
        video_dir=config.VIDEO_OUTPUT_DIR,
        video_duration=config.VIDEO_DURATION_SECONDS,
        video_max_duration=config.VIDEO_MAX_DURATION_SECONDS        
    )

    camera_service.start()

if __name__ == "__main__":
    main()
