function GetCheckBoxByTheme(a) { 
    a.forEach((i) => {
        document.getElementById("div-checkbox-" + i).style.display = "none";
    })
    var x = document.getElementById("theme-select-answer").value;
    document.getElementById("div-checkbox-" + x).style.display = "flex";
}

function GetCheckBoxCorrectAnswer(b) {
    document.getElementById("question-select-value").value = document.getElementById("question-select").value;
    const xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var c = this.responseText.slice(1, this.responseText.length - 1).split(',').map(Number);
            for(let i = 1; i <= b; i++) {
                document.getElementById("div-checkbox-value-" + i).checked = contains(c, Number(document.getElementById("div-checkbox-value-" + i).value));
            }
        
        }
    };
    xhr.open('GET', '/api/QuesToAnswer/getAnswerIdFromQuestionId?Id=' + document.getElementById("question-select-value").value,true)
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.send();

}

function GetQuestionByTheme(a) {
    a.forEach((i)=>{
        document.getElementById("div-select-questions-"+ i).style.display = "none";
    })
    var x = document.getElementById("theme-select-question").value;
    document.getElementById("div-select-questions-" + x).style.display = "flex";
}

function contains(arr, elem) {
    return arr.indexOf(elem) != -1;
}

function Func() {
    
    document.getElementById("imageGroup").addEventListener('click', e => {
        console.log(e.target.getAttribute("height") == "400px");
        if (e.target.getAttribute("height") == "400px") {
            e.target.style = (e.target.getAttribute("style") == "" ? "filter:blur(20px)" : "");
            
            document.getElementById("image-value-" + e.target.getAttribute("value")).checked = (e.target.getAttribute("style") == "" ? false : true);
        }
        
        
    });
}


function GetIdSet() {
    var x = document.getElementById("select-idGroup").value;
    var n = document.getElementById("display-id").value;
    document.getElementById("select-IdSet-" + n).style.display = "none";
    document.getElementById("select-IdSet-" + x).style.display = "flex";
    document.getElementById("display-id").value = x;

    
}




function GetAnswers(a) {
    var x = document.getElementById("select-IdSet-" + a).value;

    var l = document.getElementById("Answer-id-shown").value;

    document.getElementById("table-" + l).style.display = "none";
    document.getElementById("table-" + x).style.display = "flex";
    document.getElementById("Answer-id-shown").value = x;

}

function GetQuestions(a) {
    var x = document.getElementById("select-IdSet-" + a).value;

    var l = document.getElementById("Question-Shown").value;

    document.getElementById("div-select-questions-" + l).style.display = "none";
    document.getElementById("div-select-questions-" + x).style.display = "flex";
    document.getElementById("Question-Shown").value = x;
    document.getElementById("question-select-value-id").value = x;
    

}

function GetAnswers2(a) {
    var x = document.getElementById("select-IdSet-" + a).value;

    var l = document.getElementById("Answer-Shown").value;

    document.getElementById("div-select-answers-" + l).style.display = "none";
    document.getElementById("div-select-answers-" + x).style.display = "flex";
    document.getElementById("Answer-Shown").value = x;
    document.getElementById("answer-select-value-id").value = x;
}

function GetImages(a) {
    var x = document.getElementById("select-IdSet-" + a).value;

    var l = document.getElementById("Image-Shown").value;

    document.getElementById("div-select-images-" + l).style.display = "none";
    document.getElementById("div-select-images-" + x).style.display = "flex";
    document.getElementById("Image-Shown").value = x;
    document.getElementById("image-select-value-id").value = x;
}

function GetImage(a) {
    var x = document.getElementById("select-IdSet-" + a).value;

    var l = document.getElementById("Image-id-shown").value;

    document.getElementById("imageGroup-" + l).style.display = "none";
    document.getElementById("imageGroup-" + x).style.display = "flex";
    document.getElementById("Image-id-shown").value = x;

}