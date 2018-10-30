
var topicId = 0;

function start() {
    debugger;
    $('#QuestionAdd').modal('show');
    $("#opOne1").val("");
    $("#opTwo1").val("");
    $("#opThree1").val("");
    $("#opFour1").val("");
    $("#opCorrect1").val("");
    $("#opQuestion1").val("");
 
}

function GetTopicList() {
    $('#btnhd').hide();

    $("#gridDemo").empty();
    var count = 1;
    $.ajax
    ({
        url: '/Home/GettopicList',
        type: 'get',
        success: function (result) {

            debugger
            $("#remove").html("");
            $("#remove").append("Select Topic And SubTopic.");
            $('#gridDemo_wrapper').hide();

            $("#TopicList").html("");
            $("#TopicList").append('<option');

            //document.getElementById("#subSelect").setAttribute( "title", "Dismissible popover");
            //$(document).find("#subSelect").attr('')
            //$("subSelect").append('<a href="#" title="Dismissible popover" data-toggle="popover" data-trigger="focus" data-content="P">Click me</a>');

            $("#topTable").empty();
            $('#topTable').append('<thead><tr><th style = "text-align: center">SnNo.</th><th data-sortable="false" style ="text-align: center">Topic</th><th data-sortable="false" style ="text-align: center">Action</th></tr></thead><tbody>');
            $("#subList").empty();

            $('#showVal').show();

            $('#inptopName').val("");
            $('#AddEditTopic').empty();
            $('#AddEditTopic').append('<button class="btn-link" type="submit"><i class="fa fa-plus" aria-hidden="true"></i></button>');


            $.each(result, function (index, value) {
                $("#TopicList").append('<option value="' + value.Value + '">' +
                     value.Text + '</option>');

                $('#topTable').append('<tr><td>' + count + '</td><td>' +
                            value.Text + '</td><td><a class = "EditAnchor"style="cursor:POINTER" id ="' + value.Qid + '"  onclick="EditTopic(' + value.Value + ')"><i class="fa fa-pencil" aria-hidden="true"></i></a>|<a class = "delAnchor"style="cursor:POINTER" id="topEdit" onclick="confirmDelTopic(' + value.Value + ')"><i class="fa fa-times" aria-hidden="true"></i></a></td></tr>');
                count++;
            })

            $('.selectpicker').selectpicker('refresh');

            $('#topTable').append('</tbody>');
            $('#topTable').DataTable({
                destroy: true, paging: true, "aLengthMenu": [[5, 10, 25, 50, 100, -1], [5, 10, 25, 50, 100]],
                "iDisplayLength": 5,
            });
        },
        error: function (data) {
            debugger
            window.location.href = "/Home/Index";
            //alert("Something went wrong..")
        },
    });
}

function PauseSubList() {

    debugger
    $('#TopicAdd').modal('show');

}

function GetSubList() {
    debugger
    $('#btnhd').hide();

    topicId = $("#TopicList").val();
    var TopicName = $("#TopicList option:selected").text();
    var count = 1;
    //$("#hdnTopicID").val(topicId);
    $("#TopicName").val($("#TopicList option:selected").text());
    $.ajax
    ({
        url: '/Home/GetsubList',
        type: 'get',
        data: { 'TopicId': topicId },
        success: function (result) {

            debugger
            $("#remove").html("");
            $("#remove").append("Select Topic And SubTopic.");
            $('#gridDemo_wrapper').hide();


            $("#TopicText").empty();
            $("#TopicText").append('<label id="TopicName" name="TopicName">' + TopicName + '</label>');
            $("#subList").html("");
            $("#IdTopic").empty();
            $("#IdTopic").val(topicId);


            $("#subTable").empty();
            $('#subTable').append('<thead><tr><th style = "text-align: center">SnNo.</th><th data-sortable="false" style ="text-align: center">SubTopic</th><th data-sortable="false" style ="text-align: center">Action</th></tr></thead><tbody>');

            $("#AddSubTopic").empty();
            $('#AddSubTopic').append('<a style="cursor:POINTER" href="javascript:void(0);" onclick="PauseQuestionList()"><i class="fa fa-plus" aria-hidden="true"></i></a>');

            $('#inpsubName').val("");
            $('#showSubVal').show();
            $('#AddEdit').empty();
            $('#AddEdit').append('<button class="btn-link" type="submit"><i class="fa fa-plus" aria-hidden="true"></i></button>');

            $.each(result, function (index, value) {
                $("#subList").append('<option value="' + value.Value + '">' +
                     value.Text + '</option>');

                $('#subTable').append('<tr><td>' + count + '</td><td>' +
                             value.Text + '</td><td><a class = "EditAnchor"style="cursor:POINTER" id ="' + value.Qid + '"  onclick="EditSubTopic(' + value.Value + ')"><i class="fa fa-pencil" aria-hidden="true"></i></a>|<a class = "delAnchor"style="cursor:POINTER" id="subEdit" onclick="confirmDelSub(' + value.Value + ')"><i class="fa fa-times" aria-hidden="true"></i></a></td></tr>');


                count++;
            })
            $('.selectpicker').selectpicker('refresh');
            $('#subTable').append('</tbody>');
            $('#subTable').DataTable({
                destroy: true, paging: true, "aLengthMenu": [[5, 10, 25, 50, 100, -1], [5, 10, 25, 50, 100]],
                "iDisplayLength": 5,
            });
        },
        error: function (data) {
            debugger
            window.location.href = "/Home/Index";
            //alert("Something went wrong..")
        },
    });
}

function PauseQuestionList() {
    debugger
    //GetSubList();
    $('#SubAdd').modal('show');

}
function GetQuestionList(item) {
    $('#loader').show();
    debugger
    if (item.value != null) {
        var SubId = item.value;
        var IdintSubId = parseInt(SubId);
        $("#subTestId").val(IdintSubId);
    }
    else {
        var SubId = item;
        var IdintSubId = parseInt(SubId);
        $("#subTestId").val(IdintSubId);
    }
    var count = 1;
    $.ajax
    ({
        url: '/Home/GetQuestions',
        datatype: 'json',
        mtType: "Get",
        data: { 'subId': SubId },

        success: function (result) {
            debugger
            $('#loader').hide();
            $("#remove").html("");
            $("#gridDemo").empty();
            $('#btnhd').show();
            $("#btnhd").html("");
            $('#btnhd').append('<button type="button" class="btn btn-primary" onclick="start();">Add <i class="fa fa-plus" aria-hidden="true"></i></button>  <input type="button" value="Add Bulk" class="btn btn-primary" data-toggle="modal" data-target="#UploadXl">');

            //if (result.length > 0) {
            $('#gridDemo').show();
            $("#gridDemo").empty();
            $('#gridDemo').append('<thead><tr><th rowspan = "2" style = "text-align: center">Q.No.</th><th data-sortable="false" style ="text-align: center" rowspan = "2">Question</th><th style ="text-align: center" colspan="5">Options</th><th  data-sortable="false" style ="text-align: center" rowspan = "2">Action</th></tr><tr><th data-sortable="false" style ="text-align: center">A</th><th data-sortable="false" style ="text-align: center" >B</th><th  data-sortable="false" style ="text-align: center" >C</th><th data-sortable="false" style ="text-align: center">D</th><th data-sortable="false" style ="text-align: center">Correct</th></tr></thead>  <tbody>');



            $.each(result, function (index, value) {
                debugger
                var substr = value.QuestionName.substring(0, 15) + '.....';
                if (value.OpOne.length > 10) {
                    var subop1 = value.OpOne.substring(0, 10) + '.....';
                } else {
                    var subop1 = value.OpOne;
                }
                if (value.OpTwo.length > 10) {
                    var subop2 = value.OpTwo.substring(0, 10) + '.....';
                } else {
                    var subop2 = value.OpTwo
                }
                if (value.OpThree.length > 10) {
                    var subop3 = value.OpThree.substring(0, 10) + '.....';
                }
                else {
                    var subop3 = value.OpThree
                }
                if (value.OpFour.length > 10) {
                    var subop4 = value.OpFour.substring(0, 10) + '.....';
                }
                else {
                    var subop4 = value.OpFour
                }
                if (value.Correct.length > 10) {
                    var subcor = value.Correct.substring(0, 10) + '.....';
                }
                else {
                    var subcor = value.Correct
                }

                $('#gridDemo').append('<tr><td>' + count + '</td><td class="test"><a data-toggle="popover" data-container = "body" data-placement = "top" data-trigger="hover" data-content="' + value.QuestionName + '">' +
                           substr + '</a></td><td style="text-align: -webkit-center;"><a data-toggle="popover" data-container = "body" data-placement = "top" data-trigger="hover" data-content="' + value.OpOne + '">' +
                           subop1 + '</a></td><td  style="text-align: -webkit-center;"><a data-toggle="popover" data-container = "body" data-placement = "top" data-trigger="hover" data-content="' + value.OpTwo + '">' +
                          subop2 + '</a></td><td  style="text-align: -webkit-center;"><a data-toggle="popover" data-container = "body" data-placement = "top" data-trigger="hover" data-content="' + value.OpThree + '">' +
                           subop3 + '</a></td><td  style="text-align: -webkit-center;"><a data-toggle="popover" data-container = "body"  data-placement = "top" data-trigger="hover" data-content="' + value.OpFour + '">' +
                           subop4 + '</a></td><td  style="text-align: -webkit-center;"><a data-toggle="popover" data-container = "body" data-placement = "top" data-trigger="hover" data-content="' + value.Correct + '">' +
                           subcor + '</a></td><td  style="text-align: -webkit-center;"><a class = "EditAnchor"style="cursor:POINTER" id ="' + value.Qid + '" data-target="#EditQuestion" data-toggle="modal" onclick="EditQues(' + value.Qid + ')"><img src="/images/Edit.ico" width="17" height="17"/></a>|<a class = "delAnchor"style="cursor:POINTER" id="' + value.Qid + '" onclick="confirmFun(' + value.Qid + ')"><img src="/images/Red X.png" width="17" height="17"/></a></td></tr>');

                count++;
            })
            $('#gridDemo').append('</tbody>');
            $('[data-toggle="popover"]').popover();
            $('#gridDemo').DataTable({
                destroy: true, paging: true, "scrollX": true, responsive: true,
            });
            //}
            //else {
            //    $('#gridDemo_wrapper').hide();
            //    $("#remove").html("");
            //    $("#remove").append("No record Found!");
            //}
        },
        error: function (data) {
            debugger
            //alert("Something went wrong..")
        },
    });
}

function confirmFun(id) {
    debugger
    var SubId = $("#subList").val();
    //swal({
    //    title: "Are you sure?",
    //    type: "warning",
    //    showCancelButton: true,
    //    confirmButtonClass: "btn-danger",
    //    confirmButtonText: "Yes, delete it!",
    //    cancelButtonText: "No, cancel pls!",
    //    width: 600,
    //    padding: 100,
    //    closeOnConfirm: false,
    //    closeOnCancel: false,
    //},
    //function (isConfirm) {
    //    if (isConfirm) {
    //        swal("Deleted!", "Your data has been deleted.", "success");
    //        deleteFun(id);
    //    } else {
    //        swal("Cancelled", "Your data is safe :)", "error");
    //        //GetQuestionList(SubId);
    //    }
    //});
    if (confirm("Confirm Delete!") == true) {
        deleteFun(id);
    }
    else {
        GetQuestionList(SubId);
    }
}
function deleteFun(id) {
    debugger
    var SubId = $("#subList").val();
    var Qid = id;
    $.ajax
   ({
       url: '/Home/Delete',
       datatype: 'json',
       mtType: "Get",
       data: { 'Qid': Qid },

       success: function (result) {
           debugger
           GetQuestionList(SubId);
           //alert("Record deleted")
       },

       error: function (data) {
           debugger
           alert("Record not deleted")
       },
   });
}
function EditQues(id) {
    debugger
    var SubId = $("#subList").val();
    var Qid = id;

    $.ajax
   ({
       url: '/Home/Edit',
       datatype: 'json',
       type: "Get",
       data: { 'Qid': Qid },

       success: function (result) {
           debugger
           var option1 = result.optionOne;
           var option2 = result.optionTwo;
           var option3 = result.optionThree;
           var option4 = result.optionFour;
           var correct = result.question.optionCorrect
           $("#opQuestion").val(result.question.EditQuestion);
           $("#opCorrect").val(result.question.optionCorrect);
           $("#opOne").val(result.optionOne);
           $("#opTwo").val(result.optionTwo);
           $("#opThree").val(result.optionThree);
           $("#opFour").val(result.optionFour);
           $("#QuesId").val(result.Qid);

           switch (correct) {
               default:

                   break;
               case option1:
                   $("#opselect").val(1).change();
                   break;
               case option2:
                   $("#opselect").val(2).change();
                   break;
               case option3:
                   $("#opselect").val(3).change();
                   break;
               case option4:
                   $("#opselect").val(4).change();

           }

       },
   });
}
