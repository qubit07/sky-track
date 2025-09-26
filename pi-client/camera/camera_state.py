from enum import Enum

class CameraState(Enum):
    IDLE = "idle"
    RECORDING = "recording"
    ERROR = "error"

    def __str__(self):
        return self.value