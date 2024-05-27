// The form fields
let scenario = document.getElementById("Scenario");
let Image = document.getElementById("Image");
let Question = document.getElementById("Question");
let ChoiceA = document.getElementById("ChoiceA");
let ChoiceB = document.getElementById("ChoiceB");
let ChoiceC = document.getElementById("ChoiceC");
let ChoiceD = document.getElementById("ChoiceD");

// The Preview fields
let scenarioText = document.getElementById("ScenarioText");
let questionText = document.getElementById("QuestionText");
let choiceAText = document.getElementById("ChoiceAText");
let choiceBText = document.getElementById("ChoiceBText");
let choiceCText = document.getElementById("ChoiceCText");
let choiceDText = document.getElementById("ChoiceDText");

// the inputs
let theInputs = document.getElementsByClassName("the-inputs");
let first = false;
let second = false;
let third = false;
let fourth = false;
// Event Handlers

scenario.addEventListener('input', function () {
    console.log("inoiwx");
    scenarioText.innerText = scenario.value;
});

Question.addEventListener('input', function () {
    questionText.innerText = Question.value;
    choiceAText.innerText = Question.value;
})

ChoiceA.addEventListener('input', function () {
    let firstInput = theInputs[0];
    if (!first) {
        firstInput.style.display = "flex";
    }

    if (ChoiceA.value === null || ChoiceA.value.trim() === "") {
        firstInput.style.display = "none";
        first = false;
    }
    choiceAText.innerText = ChoiceA.value;
});

ChoiceB.addEventListener('input', function () {
    let firstInput = theInputs[1];
    if (!second) {
        firstInput.style.display = "flex";
    }

    if (ChoiceB.value === null || ChoiceB.value.trim() === "") {
        firstInput.style.display = "none";
        second = false;
    }
    choiceBText.innerText = ChoiceB.value;
});

ChoiceC.addEventListener('input', function () {
    let firstInput = theInputs[2];
    if (!third) {
        firstInput.style.display = "flex";
    }

    if (ChoiceC.value === null || ChoiceC.value.trim() === "") {
        firstInput.style.display = "none";
        third = false;
    }
    choiceCText.innerText = ChoiceC.value;
});

ChoiceD.addEventListener('input', function () {
    let firstInput = theInputs[3];
    if (!fourth) {
        firstInput.style.display = "flex";
    }

    if (ChoiceD.value === null || ChoiceD.value.trim() === "") {
        firstInput.style.display = "none";
        fourth = false;
    }
    choiceDText.innerText = ChoiceD.value;
});

function previewImage(event) {
    const input = event.target;
    const imgPreview = document.getElementById("ImgPreview");

    if (input.files && input.files[0]) {
        const reader = new FileReader();
        reader.onload = function () {
            imgPreview.src = reader.result;
        };
        reader.readAsDataURL(input.files[0]);
    } else {
        // Handle the case when no file is chosen
        imgPreview.src = ""; // Clear the preview
    }
}
