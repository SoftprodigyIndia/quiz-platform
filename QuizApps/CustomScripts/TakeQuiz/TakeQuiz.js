
var Listing = {
    GetTopicList: function () {
        var count = 1;
        $.ajax
               ({
                   url: '/Home/GettopicList ',
                   type: 'get',
                   success: function (result) {
                       debugger
                       $.each(result, function (index, value) {
                           $("#SelectTopic").append('<div class="form-group col-md-6"> <div class="panel" style="border:1px solid #ddd" ><div id="hd_' + count + '"class="panel-heading"style="background-color:#eeeeee;"></div><div id="sb_' + count + '"></div></div></div>')
                           $("#hd_"+count+"").append(' <div id="' + value.Value + '">'+ value.Text + '</div>');
                           Listing.GetSubList(value.Value, count);
                          
                           count++;
                          
                       })
                       
                   },
                   error: function (data) {
                       debugger
                       //alert("Something went wrong..")
                   },
               });
    
    },
    GetSubList: function (item,count) {
        var subCount = count;
        var SUBcounts = 1;
        debugger
        $.ajax
        ({
            url: '/Home/GetsubList',
            type: 'get',
            data: { 'TopicId': item },
            success: function (result) {
                debugger
                $("#sb_" + subCount + "").append('<div id="sbBody_' + subCount + '"class="panel-body">');
                $.each(result, function (index, value) {

                    $("#sbBody_" + subCount + "").append('<div class="row">');
                    $("#sbBody_" + subCount + "").append('' + SUBcounts + '. <a style="cursor:POINTER" onclick="Listing.GetInstruction(' + value.Value + ')">' + value.Text + '</a></div> ');
                  
                    SUBcounts++;
                })
                $("#sbBody_" + subCount + "").append('</div>');
                
            },
            error: function (data) {
                debugger
                //alert("Something went wrong..")
            },
        });
    },
    GetInstruction: function (item) {
        debugger
        //TempData["SubId"] = item;
        $("#StartSubid").val(item);
        $.ajax({
            url: '/Home/Instruction',
            data: { 'subId': item },
            dataType: "html",
            
            success: function (result) {
                debugger
                $("#instruct").html(result);
            }
        })
        
    },
    GetList: function () {
        debugger
        $.ajax({
             url: '/Home/GettopicList',
              type: 'get',
             success: function (result) {
                 debugger
                 location.reload();
               
             }
        })
    },
   

}