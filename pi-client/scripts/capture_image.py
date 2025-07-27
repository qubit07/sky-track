# capture_image.py
# This script captures an image using the Picamera2 library.
from picamera2 import Picamera2, Preview
import time

picam2 = Picamera2()
camera_config = picam2.create_preview_configuration()
picam2.configure(camera_config)

picam2.start()
time.sleep(2)   # Allow camera to warm up
print("Capturing image...")
picam2.capture_file("picture_001.jpg")
print("Image captured and saved as picture_001.jpg")
picam2.close()