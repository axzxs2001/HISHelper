//每页的记录数
//var recordPerPage = 20;

//每个分页组的页数
//var pagePerGroup = 15;
var currentPageIndex = 1;
function LoadMessage(currentPageIndex) {
    $("#arrows").hide();
    $("#listcontrol").show();

    //每页表格的行数
    var RecordPerPage = 15;
    //每个分页组的页数
    var pagePerGroup = 10;
    $.post('queryzkcpproducts', {
        currentPageIndex: currentPageIndex,
        recordPerPage: RecordPerPage,
        pagePerGroup: pagePerGroup
    }, function (result) {
        var obj = result;
        SetPagination(currentPageIndex, obj, 'LoadMessage', RecordPerPage, pagePerGroup);
    });
}


//生成分页控件
function GetPagination(CurrentPageIndex, TotalPage, functionName, RecordCount, pagePerGroup) {
    if (CurrentPageIndex > TotalPage || CurrentPageIndex <= 0) {
        return;
    }
    else {
        var htmlContent = "";

        //每页的起始索引
        var baseIndex = parseInt((CurrentPageIndex - 1) / pagePerGroup) * pagePerGroup + 1;
        //处理当前页是第一页情况
        if (CurrentPageIndex <= pagePerGroup) {
            htmlContent = htmlContent + '<div class="row"><div class="col-md-6"><ul class="pagination" style="float: right;margin-top: 7px;">';
            htmlContent = htmlContent + '<li class="disabled"><a  href="javascript:void(0);" >&laquo;</a></li>';
        } else {
            htmlContent = htmlContent + '<div class="row"><div class="col-md-6"><ul class="pagination" style="float: right;margin-top: 7px;">';
            htmlContent = htmlContent + '<li><a  href="javascript:void(0);" onclick="' + functionName + '(' + (baseIndex - 1) + ') ">&laquo;</a></li>';
        }
        //加载其他页
        for (var i = baseIndex; i < baseIndex + pagePerGroup; i++) {
            if (i > TotalPage)
            { break; }

            if (i === CurrentPageIndex) {
                htmlContent = htmlContent + ' <li class="active"><a href="javascript:void(0);">' + i + '</a></li>'

            } else {
                htmlContent = htmlContent + '<li><a  href="javascript:void(0);"  onclick="' + functionName + '(' + i + ')">' + i + '</a></li>'
            }
        }
        //处理当前页是最后页情况
        if (CurrentPageIndex >= parseInt((TotalPage - 1) / pagePerGroup) * pagePerGroup + 1) {
            htmlContent = htmlContent + '<li class="disabled"><a  href="javascript:void(0);">&raquo;</a></li>';
        } else {
            htmlContent = htmlContent + '<li><a  href="javascript:void(0);" onclick="' + functionName + '(' + (baseIndex + pagePerGroup) + ')">&raquo;</a></li>';
        }
        htmlContent = htmlContent + '</ul></div>';
        htmlContent = htmlContent + '<span class="col-md-6" style="text-align: left; color:#fff;font-size: 16px;line-height:40px;">';
        htmlContent = htmlContent + "查询到" + RecordCount + "条记录，共" + TotalPage + "页";
        htmlContent = htmlContent + '</span></div>'

        //htmlContent = htmlContent + '<ul class="uis">';
        //htmlContent = htmlContent +'<li class="lt" id="it1"; style= "color:#fff; font-size:16px" >' + CurrentPageIndex + '/' + TotalPage + '页</li>';
        //htmlContent = htmlContent +'<li class="lt"><a href="javascript:void(0);" id="itit2" onclick="' + functionName - '(' + i + ')">上一页</a></li>';
        //htmlContent = htmlContent +'<li class="lt" ><a href="javascript:void(0);" id="it2;"  onclick="' + functionName + '(' + i + ')">下一页</a></li>';
        //htmlContent = htmlContent +'<li class="lt"><input type="text" id="it3"></li>';
        //var number = $("#it3").val();
        //htmlContent = htmlContent +' <li class="lt" id="it4"><a href="javascript:void(0);"  onclick="' + functionName + '(' + number + ')">跳 转</a></li>';
        //htmlContent = htmlContent + '</ul>';
        return htmlContent;
    }
}
//加载数据动态添加
function Dynamically(obj) {

    var str = '';
    if (obj.List.length !== 0) {
        str += '<thead>';
        str += '<tr>';
        str += '<th><input type="checkbox"><span></span>标题</th>';
        str += ' <th><span></span>优先级</th>';
        str += ' <th></th>';
        str += '<th><span></span>发布人</th>';
        str += '<th><span></span>审核状态</th>';
        str += '<th><span></span>发布时间</th>';
        str += '</tr>';
        str += '</thead>';
        $(obj.List).each(function (index, item) {
            str += '<thead id="' + item["Id"] + '" value="' + item["Id"] + '">';
            str += '<tr  value="' + item["Id"] + '">';
            str += '<th id="zkcp' + item["Id"] + '"><input type="checkbox" value="' + item["Id"] + '" style="margin-right: 8px;">' + item["ProductName"] + '</th>';
            str += '<th>' + item["行号"] + '</th>';
            str += '<th></th>';
            str += '<th></th>';
            str += '<th></th>';
            str += '<th></th>';
            str += '</tr>';
            str += '</thead>';
            QueryzkcpCount(item["Id"]);   //获得产品ID查询需求条数
        });
        $(".tablezkcp").html(str);
    }
    else {
        $(".tablezkcp").html('<span style="font-weight:bold">无数据</span>');
    }
}
//#region  只看产品页面查询需求条数
function QueryzkcpCount(id) {
    $.ajax({
        type: "POST",
        url: '/queryrequestcount',
        data: { id: id },
        success: function (dataBack) {
            $("#zkcp" + id + "").append("（" + dataBack.data + "条）");
        },
        error: function (error) {
            alert(error.status + error.statusText)
        }
    });
}
//#endregion

//加载分页条
function SetPagination(CurrentPageIndex, obj, functionName, recordPerPage, pagePerGroup) {
    //得到记录数
    var RecordCount = obj.RecordCount;
    //得到总页数
    var TotalPage = Math.ceil(RecordCount / recordPerPage);
    Dynamically(obj);
    //查询到数据才显示分页信息
    if (RecordCount > 0) {
        var paging = GetPagination(CurrentPageIndex, TotalPage, functionName, RecordCount, pagePerGroup);;
        //显示页脚分页信息
        $(".whyy").html(paging);
    }
    else {
        var pageStr = '<div class="row"><div class="col-md-6"><ul class="pagination" style="float: right;margin-top: 7px;"></ul></div><span class="col-md-6" style="text-align: left; color: #fff;font-size: 16px;line-height:40px;">';
        pageStr = pageStr + "查询到" + RecordCount + "条记录，共" + TotalPage + "页";
        pageStr = pageStr + '</span>';


        //$("#it1").html(CurrentPageIndex + "/" + TotalPage + "页");
        //$("#itit2").click(function () {
        //    alert(currentPageIndex.val() - 1);
        //    LoadMessage(currentPageIndex - 1);
        //});
        //$("#it2").click(function () {
        //    LoadMessage(currentPageIndex + 1);
        //});
        //$("#it4").click(function () {
        //    if (!$("#it3").val().match(/^[0-9]*$/)) {

        //        layer.msg("请输入数字！", { icon: 2 });
        //        return false;
        //    }

        //    var number = $("#it3").val();
        //    LoadMessage(currentPageIndex + (number - 1));
        //    $("#it3").val("");
        //});
        $(".tablezkcp").append(pageStr);
    }
}