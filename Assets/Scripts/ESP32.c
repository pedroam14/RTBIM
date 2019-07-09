#define ANALOG_PIN_0 36
#define ANALOG_PIN_1 39
int sensor1,sensor2;
String s1 = "Sensor 1: ";
String s2 = "Sensor 2: ";
String aux;
void setup()
{
  Serial.begin(115200);
  delay(1000); 
  Serial.println("ESP32 Analog Test");
}

void loop()
{
  aux = s1 + analogRead(ANALOG_PIN_0);
  Serial.println(aux);
  aux = s2 + analogRead(ANALOG_PIN_1);
  Serial.println(aux);
  delay(1000);
}