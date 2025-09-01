import paho.mqtt.client as mqtt
from mqtt_topics import MqttTopics


class MqttClient:
    def __init__(self, host='localhost', port=1883, client_id='pi-client'):
        self.host = host
        self.port = port
        self.client_id = client_id
        self.client = mqtt.Client(client_id=self.client_id)
        self.connected = False

        # event handlers
        self.client.on_connect = self._on_connect
        self.client.on_disconnect = self._on_disconnect
        self.client.on_message = self._on_message

    def _on_connect(self):
        self.connected = True
        print(f"Connected to MQTT broker at {self.host}:{self.port} with client ID {self.client_id}")

        # Subscribe to default topics
        self.subscribe(MqttTopics.CAMERA_CONTROL)
        self.subscribe(MqttTopics.CAMERA_STATE)

    def _on_disconnect(self):
        self.connected = False
        print("Disconnected from MQTT broker")

    def _on_message(self, client, userdata, msg):
        print(f"Received message: {msg.payload.decode()} on topic {msg.topic}")

    def publish(self, topic: MqttTopics, message: str):
        if not self.connected:
            raise Exception("Client is not connected to the broker")
        print(f"Published message '{message}' to topic '{topic}'")

    def subscribe(self, topic: MqttTopics):
        if not self.connected:
            raise Exception("Client is not connected to the broker")
        self.client.subscribe(topic)
        print(f"Subscribed to topic '{topic}'")

    def unsubscribe(self, topic: MqttTopics):
        if not self.connected:
            raise Exception("Client is not connected to the broker")
        self.client.unsubscribe(topic)
        print(f"Unsubscribed from topic '{topic}'")

    def start(self):
        self.client.connect(self.host, self.port)
        self.client.loop_start()

    def stop(self):
        self.client.loop_stop()
        self.client.disconnect()