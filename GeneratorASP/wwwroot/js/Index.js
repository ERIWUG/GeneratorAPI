function MyFunc(a) {
    for (let i = 1; i <= a; i++) {
        document.getElementById("div-checkbox-" + i).style.display = "none";
    }
    var x = document.getElementById("theme-select").value;
    document.getElementById("div-checkbox-" + x).style.display = "flex";
}

function MyFunc1(){
     
        document.getElementById("question-select-value").value = document.getElementById("question-select").value;
}