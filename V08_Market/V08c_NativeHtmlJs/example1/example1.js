let counter = 0;
let running = false;
let counterDisplay = null;
let slider = null;
let sliderValue = null;

function onInit(){
    counterDisplay = document.querySelector("#counterDisplay");
    slider = document.querySelector("#speedSlider");
    sliderValue = document.querySelector("#sliderValue");

    if (counterDisplay == null)
        throw 'counterDisplay == null';
    if (slider == null)
        throw 'slider == null';
    if (sliderValue == null)
        throw 'sliderValue == null';

    slider.oninput = () => {
        console.log(` Nova vrijednost za varijablu slider.value = ${slider.value}.`)
        sliderValue.textContent = slider.value;
    };
}        

function tick() {
    if (!running) return;

    counter++;
    counterDisplay.value = counter;

    // svaki put nanovo uzima interval iz slidera
    const interval = parseInt(slider.value, 10);
    console.log(`Novi setTimeout za interval = ${interval}.`)
    setTimeout(()=>tick(), interval); //<--- dorađeno nakon nastave - da bude u skladu sa example1.component
}

function startTimer() {
    if (running) return;
    running = true;
    tick();
}

function stopTimer() {
    running = false;
}

document.addEventListener("DOMContentLoaded", onInit);