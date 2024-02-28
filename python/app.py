from flask import Flask, request, jsonify
import joblib

app = Flask(__name__)

# Load logistic regression model
logRes_model = joblib.load('hearth_disease_py/logRes_Model.pkl')

# Load LightGBM model
lightGB_model = joblib.load('diabetes_py/lightGB_model.pkl')

@app.route('/predict/logistic', methods=['POST'])
def predict_logistic():
    data = request.json
    features = [data['Age'], data['Gender'], data['ChestPainType'],data['RestingBloodPressure'],data['SerumCholesterol'],
                 data['FastingBloodSugar'],
                data['RestingElectrocardio'],data['MaxHearthRate'], data['ExerciseAngina'], data['OldPeak'], data['SlopeST'],
                data['MajorVessels']]
    logRes_prediction = logRes_model.predict([features])

    return jsonify({'logRes_prediction': int(logRes_prediction[0])})

@app.route('/predict/lightgbm', methods=['POST'])
def predict_lightgbm():
    data = request.json
    features = [data['BMI'],data['Age'],   data['GenHlth'],
                data['Income'], data['HighChol'],data['Sex'], data['HearthDiseaseorAttack'],
                data['HvyAlcoholConsump'], data['CholCheck'], data['PhsylHlth']]
    lightGB_prediction = lightGB_model.predict([features])

    return jsonify({'lightGB_prediction': int(lightGB_prediction[0])})

if __name__ == '__main__':
    app.run(port=5000)
