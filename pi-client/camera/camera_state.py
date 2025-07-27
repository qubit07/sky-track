from enum import Enum

class CameraState(Enum):
    IDLE = "idle"
    RECORDING = "recording"
    MOTION_DETECTED = "motion_detected"
    ERROR = "error"

    def __str__(self):
        return self.value