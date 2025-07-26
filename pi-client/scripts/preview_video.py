from picamera2 import Picamera2, Preview
import time

picam2 = Picamera2()
picam2.configure(picam2.create_preview_configuration())

try:
    print("Starting camera preview...")
    picam2.start_preview(Preview.QTGL)
    picam2.start()
    print("Camera preview started. Press Ctrl+C to stop.")
    time.sleep(30) # Keep the preview running for 30 seconds
    picam2.stop_preview()
    picam2.stop()
    print("Camera preview stopped.")
except Exception as e:
    print("Failed to start camera preview.")
    print(f"GUI must be running to display the preview.")
    print(f"An error occurred: {e}")
