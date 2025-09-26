# main.py

import config, logging
from camera.camera_service import CameraService

def main():
    logging.basicConfig(
        level=logging.INFO,
        format="%(asctime)s [%(levelname)s] %(name)s - %(message)s"
    )

    camera_service = CameraService(
        video_dir=config.VIDEO_OUTPUT_DIR,
        video_duration=config.VIDEO_DURATION_SECONDS,
        video_max_duration=config.VIDEO_MAX_DURATION_SECONDS        
    )

    camera_service.start()

if __name__ == "__main__":
    main()
