function MyFunc(a) {
    for (let i = 1; i <= a; i++) {
        document.getElementById("div-checkbox-" + i).style.display = "none";
    }
    var x = document.getElementById("theme-select").value;
    document.getElementById("div-checkbox-" + x).style.display = "flex";
}

function MyFunc1(a,b){
    console.log(a);
    document.getElementById("question-select-value").value = document.getElementById("question-select").value;



    for (let i = 1; i <= b; i++) {
        document.getElementById("div-checkbox-value-"+i).checked==contains(a,document.getElementById("div-checkbox-value-"+i).value)
    }
}

function contains(arr, elem) {
    return arr.indexOf(elem) != -1;
}