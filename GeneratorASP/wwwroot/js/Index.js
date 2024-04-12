function MyFunc(a) {
    for (let i = 1; i <= a; i++) {
        document.getElementById("div-checkbox-" + i).style.display = "none";
    }
    var x = document.getElementById("theme-select").value;
    document.getElementById("div-checkbox-" + x).style.display = "flex";
}

function Func(b) {
    document.getElementById("question-select-value").value = document.getElementById("question-select").value;
    const xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var c = this.responseText.slice(1, this.responseText.length - 1).split(',').map(Number);
            alert(c);
            for(let i = 1; i <= b; i++) {
                document.getElementById("div-checkbox-value-" + i).checked = contains(c, Number(document.getElementById("div-checkbox-value-" + i).value));
            }
        
        }
    };
    xhr.open('GET', '/api/QuesToAnswer/getAnswerIdFromQuestionId?Id=' + document.getElementById("question-select-value").value,true)
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.send();

}
function contains(arr, elem) {
    return arr.indexOf(elem) != -1;
}
