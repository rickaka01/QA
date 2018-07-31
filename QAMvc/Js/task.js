$(document).ready(function () {
    ShowData();
    $("#btnOK").click(function () { DoOK(); });
});

/*查询*/
function ShowData() {
    var url = "/api/Task/Get";
    url += "?cid=" + getAllQueryResolveString().cid;
    url += "&tid=" + getAllQueryResolveString().tid;
    url += "&seq=" + getAllQueryResolveString().seq;
    $.ajax({
        url: url,
        contentType: "application/json",
        dataType: "json",
        method: "post",
        async: true,
        data: "{}",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("sessionID", window.sessionStorage["sessionID"]);
            xhr.setRequestHeader("userID", window.sessionStorage["userID"]);
        },
        success: function (result) {
            // 这里得到tid和seq
            window.localStorage["tid"] = result.tid;
            window.localStorage["seq"] = result.i;
            $("#content").find("td").each(function (index) {
                if (index == 0) $(this).append(result.p);
                if (index == 1) {
                    var s = getAllQueryResolveString().seq;
                    if (s == undefined) {
                        $(this).append("第01题");
                    }
                    else
                        $(this).append("第0" + s+"题");
                }
                if (index == 2) $(this).append(result.p1);
                if (index == 3) $(this).append(result.p2);
                if (index == 4) $(this).append(result.p3);
                if (index == 5) $(this).append(result.p4);
            });
        },
        error: function (request, error) {
            alert(error + "-1");
        }
    });
}

/*提交*/
/*提交之后再调用查询*/
function DoOK() {
    var url = "/api/Task/Do";
    var v = window.localStorage["tid"];
    var v2;
    if ($("#A").prop("checked") == true) {
        v2 = "A";
    }
    if ($("#B").prop("checked") == true) {
        v2 = "B";
    }
    if ($("#C").prop("checked") == true) {
        v2 = "C";
    }
    if ($("#D").prop("checked") == true) {
        v2 = "D";
    }

    var cid = getAllQueryResolveString().cid;
    var tid = window.localStorage["tid"];
    var seq = window.localStorage["seq"];
    var seq1 = new Number(seq);
   
    var s = "{";
    s += "\"p\":\"" + v + "\",";
    s += "\"p1\":\"" + seq + "\",";
    s += "\"p2\":\"" + v2 + "\"";
    s += "}";

    //获取下一个
    var objurl = "Task.html?cid=" + cid + "&tid=" + tid + "&seq=" + (seq1 + 1);

    $.ajax({
        url: url,
        contentType: "application/json",
        dataType: "json",
        method: "post",
        async: true,
        data: s,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("sessionID", window.sessionStorage["sessionID"]);
            xhr.setRequestHeader("userID", window.sessionStorage["userID"]);
        },
        success: function (result) {
            //获得返回值task和seq
            if (result == true) {
                if (seq1 == "5") {
                    window.location = "score.html?tid=" + tid;
                }
                else {
                    window.location = objurl;
                }
            }
            else
                alert("答题错误!");
        },
        error: function (request, error) {
            alert(error + "-1");
        }
    });
}