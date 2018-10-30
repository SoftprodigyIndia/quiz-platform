
/**** Quiz ****/
var questions = [{
    question: "",
    choices: ["", "", "", ""],
    correctAnswer: 0
}]
var qId = 0;
var progressCount = 1;
//$(document).ready(function () {
function Test() {
    debugger
    var item = $("#StartSubid").val();
    var i = 0;

    $.ajax
    ({
        url: '/Home/GetQuestions',
        type: 'get',
        data: { 'subId': 0 },
        success: function (result) {
            $.each(result, function (index, value) {
                debugger
                qid = value.Qid
                questions[i] = [{
                    question: [value.QuestionName],
                    choices: [value.OpOne, value.OpTwo, value.OpThree, value.OpFour],
                    correctAnswer: value.Correct
                }]

                i++;
            })
            displayCurrentQuestion();
        }
    });
}

//displayCurrentQuestion();
//$(this).find(".quizMessage").hide();

//// On clicking next, display the next question
//$(this).find(".nextButton").on("click", function () {
//    debugger
//    if (!quizOver) {

//        value = $("input[type='radio']:checked").val();

//        if (value == undefined) {
//            $(document).find(".quizMessage").text("Please select an answer");
//            $(document).find(".quizMessage").show();
//        } else {
//            // TODO: Remove any message -> not sure if this is efficient to call this each time....
//            $(document).find(".quizMessage").hide();

//            if (value == questions[currentQuestion].correctAnswer) {
//                correctAnswers++;
//            }

//            currentQuestion++; // Since we have already displayed the first question on DOM ready
//            if (currentQuestion < questions.length) {
//                displayCurrentQuestion();
//            } else {
//                displayScore();
//                //                    $(document).find(".nextButton").toggle();
//                //                    $(document).find(".playAgainButton").toggle();
//                // Change the text in the next button to ask if user wants to play again
//                $(document).find(".nextButton").text("Play Again?");
//                quizOver = true;
//            }
//        }
//    } else { // quiz is over and clicked the next button (which now displays 'Play Again?'
//        quizOver = false;
//        $(document).find(".nextButton").text("Next Question");
//        resetQuiz();
//        displayCurrentQuestion();
//        hideScore();
//    }
//});
//QuizStart();
//displayCurrentQuestion();
//    }


//var questions = [{
//    question: "What is the population of Brazil?",
//    choices: ["145 million", "199 million", "182 million", "205 million"],
//    correctAnswer: 1
//}, {
//    question: "What is 27*14?",
//    choices: ["485", "634", "408", "528"],
//    correctAnswer: 2
//}, {
//    question: "What is the busiest train station in the world?",
//    choices: ["Grand Central, NY", "Shibuya, Tokyo", "Beijing Central, Chine", "Gard du Nord, Paris"],
//    correctAnswer: 1
//}, {
//    question: "What is the longest river?",
//    choices: ["Nile", "Amazon", "Mississippi", "Yangtze"],
//    correctAnswer: 0
//}, {
//    question: "What is the busiest tube station in the London?",
//    choices: ["Waterloo", "Baker Street", "Kings Cross", "Victoria"],
//    correctAnswer: 0
//}, {
//    question: "",
//    choices: [],
//    correctAnswer: 0
//}];



var currentQuestion = 0;
var correctAnswers = 0;
var quizOver = false;
var count = 1;
var totalTime = '';
//var nowMinutes = 0;
//var nowSeconds = 0;
var today = 0;
var start = new Date().getTime();
var remaining = 0;

$(document).ready(function () {
    //function QuizStart(){

    debugger
    // Display the first question
    Test();

    debugger
    $('#headerArea').hide();
    $(this).find(".quizMessage").hide();

    // On clicking next, display the next question
    $(this).find(".nextButton").on("click", function () {
        debugger
        if (!quizOver) {

            value = $("input[type='radio']:checked").val();

            if (value == undefined) {
                $(document).find(".quizMessage").text("Please select an answer");
                $(document).find(".quizMessage").show();
            } else {
                // TODO: Remove any message -> not sure if this is efficient to call this each time....
                $(document).find(".quizMessage").hide();

                if (value == questions[currentQuestion][0].correctAnswer) {
                    correctAnswers++;
                }

                currentQuestion++;
                
                // Since we have already displayed the first question on DOM ready
                if (currentQuestion < (questions.length) - 1) {
                    displayCurrentQuestion();
                    progressBar();
                }
                else if (currentQuestion == (questions.length) - 1) {
                    displayCurrentQuestion();
                    progressBar();
                    $(document).find(".nextButton").text("Submit");
                }
                else {

                    remaining = -1;

                    //$(document).find(".nextButton").text("Play Again?");



                    quizOver = true;
                }
            }
        } else { // quiz is over and clicked the next button (which now displays 'Play Again?'
            quizOver = false;
            $(document).find(".nextButton").text("Next Question");
            resetQuiz();
            displayCurrentQuestion();
            hideScore();
        }
    });

});

// This displays the current question AND the choices
function displayCurrentQuestion() {
    debugger
    console.log("In display current Question");

    var question = questions[currentQuestion][0].question;
    
    var questionClass = $(document).find(".quizContainer > .question");
    
    //var quetsionC = document.getElementById("txt").tagName;
    var choiceList = $(document).find(".quizContainer > .choiceList");
    var numChoices = questions[currentQuestion][0].choices.length;
   
    // Set the questionClass text to the current question
    $(questionClass).text(question);
   
    auto_grow(questionClass[0]);
    //$(questionClass).text("<h1>Qno.</h1>");
    //$(questionClass).append('<h3>Qno.' + count + '</h3>');
    //$(questionClass).append(' ' + question + ' ');
    count++;
    // Remove all current <li> elements (if any)
    $(choiceList).find("li").remove();

    var choice;
    var temp = 1;
    for (i = 0; i < numChoices; i++) {
        
        choice = questions[currentQuestion][0].choices[i];
       // $('<li><input type="radio" value="' + choice + '" name="radio"  class="radio"/><label for="radio' + temp + '">' + choice + '</label></li>').appendTo(choiceList);
        $('<li class="listyle"><input type="radio" id ="radio' + temp + '" value="' + choice + '" name="radio"  class="radio"/><label for="radio' + temp + '" class="labstyle">' + choice + '</label></li>').appendTo(choiceList);
        //var choiceClass = $(document).find("quizContainer > .choiceList > .choiceInd");
        //auto_grow(choiceClass[0]);
       temp =  temp+1;
    }
}

function resetQuiz() {
    currentQuestion = 0;
    correctAnswers = 0;
    hideScore();
}

function displayScore() {
    var newUrl = '@Url.Action("TakeQuiz","Home")';
    debugger
    $("correctAns").val(correctAnswers);
    //$("")
    var nowt = new Date().getTime();
    var duration = (nowt - start) / 1000;

    var nowMinutes = ~~(duration / 60);
    var nowSeconds = ~~(duration % 60);
    totalTime = "" + format(nowMinutes) + ":" + format(nowSeconds) + "";
   $.ajax({
        url: '/Home/GetScore',
        data: {
            'correctAnswers': correctAnswers,
            'totalTime': totalTime,
            'today': today,
            'currentQuestion': currentQuestion,
            'quesId': qid
        },
        dataType: "html",

        success: function (result) {
            debugger
            swal({title:"Done!", 
                text:"Your Quiz is Submitted Successfully!",
                type: "success"
            },
            function(){
                window.location.href = "/Home/TakeQuiz";
            });
           
            //$("#instruct").html(result);
        }
    })
    //$(document).find(".quizContainer > .result").text("You scored: " + correctAnswers + " out of: " + questions.length);
    //$(document).find(".quizContainer > .result").show();
}

function hideScore() {
    $(document).find(".result").hide();
}

countdown(1200);

function countdown(seconds) {

    // current timestamp.
    var now = new Date().getTime();
    var target = new Date(now + seconds * 1000);
    // update frequency; note, this is flexible, and when the tab is
    // inactive, there are no guarantees that the countdown will update
    // at this frequency.
    var update = 500;

    var int = setInterval(function () {

        // current timestamp
        var now = new Date();
        // remaining time, in seconds

        if (remaining >= 0) {
            remaining = (target - now) / 1000;
        }
        else {

            clearInterval(int);
            displayScore();
            return;
        }

        //today = new Date();
        var nowdate = new Date();
       
        var hrs = ("0" + nowdate.getHours()).slice(-2);
        var min = ("0" + nowdate.getMinutes()).slice(-2);
        var sec = ("0" + nowdate.getSeconds()).slice(-2);
        var day = ("0" + nowdate.getDate()).slice(-2);
        var month = ("0" + (nowdate.getMonth() + 1)).slice(-2);
        today = now.getFullYear() + "/" + (month) + "/" + (day) + " " + (hrs) + ":" + (min) + ":" + (sec);
      

        // format
        var minutes = ~~(remaining / 60);
        var seconds = ~~(remaining % 60);

        //var nowMinutes = ~~(tnow / 60);
        //var nowSeconds = ~~(tnow % 60);
        //totalTime = ""+nowMinutes + ":" + nowSeconds;

        // display
        document.getElementById("countdown").innerHTML
        = format(minutes) + ":" + format(seconds);
    }, update);
}

function format(num) {
    return num < 10 ? "0" + num : num;
}

function auto_grow(element) {
    debugger
    
    element.style.height = "5px";
    element.style.height = (element.scrollHeight) + "px";
}

function progressBar() {
    var x = document.getElementById("q" + progressCount + "").classList;
    var progressCount2 = progressCount + 1;
    var y = document.getElementById("q" + progressCount2 + "").classList;
    if (x.contains("active")) {
        x.remove("active");
        x.add("complete");
        if (y.contains("disabled")) {
            y.remove("disabled");
            y.add("active");
        }
    } else {

    }
    progressCount = progressCount + 1;
}




