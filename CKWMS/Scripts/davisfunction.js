﻿// 对Date的扩展，将 Date 转化为指定格式的String
// 月(M)、日(d)、小时(h)、分(m)、秒(s)、季度(q) 可以用 1-2 个占位符， 
// 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字) 
// 例子： 
// (new Date()).Format("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423 
// (new Date()).Format("yyyy-M-d h:m:s.S")      ==> 2006-7-2 8:9:4.18 
Date.prototype.Format = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}
//在String对象中扩展一个toDate方法
String.prototype.toDate = function () {
    var dateMilliseconds;
    if (isNaN(this)) {
        //使用正则表达式将日期属性中的非数字（\D）删除
        dateMilliseconds = this.replace(/\D/igm, "");
    } else {
        dateMilliseconds = this;
    }
    //实例化一个新的日期格式，使用1970 年 1 月 1 日至今的毫秒数为参数
    return new Date(parseInt(dateMilliseconds));
}
//显示加载等待
function ShowWait() {
    $("#loading").show();
}
//隐藏加载等待
function HiddenWait() {
    $("#loading").hide();
}
//将check的值写入隐含变量，选种为1,不选种为0
function CheckToHidden(oCheck, oHidden) {
    oCheck = eval("document.all." + oCheck);
    oHidden = eval("document.all." + oHidden);
    if (typeof (oCheck) != "object" || typeof (oHidden) != "object")
        return;
    if (oCheck.tagName != "INPUT" || oHidden.tagName != "INPUT")
        return;
    oHidden.value = (oCheck.checked) ? 1 : 0;
}
//将隐含变量的值显示在check，1为选种,0不选种
function HiddenToCheck(oHidden, oCheck) {
    var oCheck = eval("document.all." + oCheck);
    var oHidden = eval("document.all." + oHidden);
    if (typeof (oCheck) != "object" || typeof (oHidden) != "object")
        return;
    if (oCheck.tagName != "INPUT" || oHidden.tagName != "INPUT")
        return;
    oCheck.checked = (oHidden.value > 0) ? true : false;
}
//将隐含变量的值显示在check，3为选种,0不选种
function HiddenToCheck2(oHidden, oCheck) {
    var oCheck = eval("document.all." + oCheck);
    var oHidden = eval("document.all." + oHidden);
    if (typeof (oCheck) != "object" || typeof (oHidden) != "object")
        return;
    if (oCheck.tagName != "INPUT" || oHidden.tagName != "INPUT")
        return;
    oCheck.checked = (oHidden.value == 3) ? true : false;
}
//将oRadio的值写入隐含变量，选种为1,不选种为0
function RadioToHidden(oRadio1, oRadio2, oHidden) {
    var oRadio1 = eval("document.all." + oRadio1);
    var oRadio2 = eval("document.all." + oRadio2);
    var oHidden = eval("document.all." + oHidden);
    if (typeof (oRadio1) != "object" || typeof (oRadio2) != "object" || typeof (oHidden) != "object")
        return;
    if (oRadio1.tagName != "INPUT" || oRadio2.tagName != "INPUT" || oHidden.tagName != "INPUT")
        return;
    oHidden.value = (oRadio1.checked) ? 1 : 0;
}
//将oRadio的值写入隐含变量，选种为1,不选种为2
function RadioToHidden2(oRadio1, oRadio2, oHidden) {
    var oRadio1 = eval("document.all." + oRadio1);
    var oRadio2 = eval("document.all." + oRadio2);
    var oHidden = eval("document.all." + oHidden);
    if (typeof (oRadio1) != "object" || typeof (oRadio2) != "object" || typeof (oHidden) != "object")
        return;
    if (oRadio1.tagName != "INPUT" || oRadio2.tagName != "INPUT" || oHidden.tagName != "INPUT")
        return;
    oHidden.value = (oRadio1.checked) ? 1 : 2;
}
//将隐含变量的值显示在oRadio1，1为选种,0不选种
function HiddenToRadio(oHidden, oRadio1, oRadio2) {
    var oRadio1 = eval("document.all." + oRadio1);
    var oRadio2 = eval("document.all." + oRadio2);
    var oHidden = eval("document.all." + oHidden);
    if (typeof (oRadio1) != "object" || typeof (oRadio2) != "object" || typeof (oHidden) != "object")
        return;
    if (oRadio1.tagName != "INPUT" || oRadio2.tagName != "INPUT" || oHidden.tagName != "INPUT")
        return;
    oRadio1.checked = (oHidden.value == 1) ? true : false;
    oRadio2.checked = (oHidden.value == 1) ? false : true;
}
//将oSelect的值写入隐含变量，选种为1,不选种为0
function SelectToHidden(oSelect, oHidden) {
    var oSelect = eval("document.all." + oSelect);
    var oHidden = eval("document.all." + oHidden);
    if (typeof (oSelect) != "object" || typeof (oHidden) != "object")
        return;
    if (oSelect.tagName != "SELECT" || oHidden.tagName != "INPUT")
        return;
    var item = new Array();
    for (var i = 0; i < oSelect.options.length; i++) {
        item[i] = oSelect.options.text;
    }
    oHidden.value = item.toString();
}
//将隐含变量的值显示在oSelect,值以逗号分开，
function HiddenToSelect(oHidden, oSelect) {
    var oSelect = eval("document.all." + oSelect);
    var oHidden = eval("document.all." + oHidden);
    if (typeof (oSelect) != "object" || typeof (oHidden) != "object")
        return;
    if (oSelect.tagName != "SELECT" || oHidden.tagName != "INPUT")
        return;
    var ooption = new Array();
    var item = new Array();
    item = oHidden.value.split(',');
    while (oSelect.options.length != 0) {
        for (var i = 0; i < oSelect.options.length; i++)
            oSelect.options.remove(i);
        oSelect.options.length = oSelect.options.length / 2;
    }
    for (var i = 0; i < item.length; i++) {
        ooption[i] = document.createElement("option");
        ooption[i].text = item[i];
        oSelect.add(ooption[i]);
    }
}
//将以‘|’分开的字符串的值显示在oSelect中。
function StringToSelect(sString, oSelect) {
    oSelect = eval("document.all." + oSelect);
    if (typeof (oSelect) != "object" || typeof (sString) != "string")
        return;
    if (oSelect.tagName != "SELECT")
        return;
    var ooption = new Array();
    var item = new Array();
    item = sString.split('|');
    while (oSelect.options.length != 0) {
        for (var i = 0; i < oSelect.options.length; i++)
            oSelect.options.remove(i);
        oSelect.options.length = oSelect.options.length / 2;
    }
    for (var i = 0; i < item.length; i++) {
        ooption[i] = document.createElement("option");
        ooption[i].text = item[i];
        oSelect.add(ooption[i]);
    }
}
//将caStr显示在oSelect值以逗号分开，1为选种,0不选种
function StrToSelect(caStr, oSelect) {
    var oSelect = eval("document.all." + oSelect);
    if (typeof (oSelect) != "object" || caStr.length == 0)
        return;
    if (oSelect.tagName != "INPUT")
        return;
    var ooption = new Array();
    var item = new Array();
    item = caStr.split(',');
    while (oSelect.options.length != 0) {
        for (var i = 0; i < oSelect.options.length; i++)
            oSelect.options.remove(i);
        oSelect.options.length = oSelect.options.length / 2;
    }
    for (var i = 0; i < item.length; i++) {
        ooption[i] = document.createElement("option");
        ooption[i].text = item[i];
        oSelect.add(ooption[i]);
    }
}
//string to select include text and value
function StringToSelect(sStrText, sStrValue, oSelect) {
    var oSelect = eval("document.all." + oSelect);
    if (typeof (oSelect) != "object" || typeof (sStrText) != "string" || typeof (sStrValue) != "string")
        return;
    if (oSelect.tagName != "SELECT")
        return;
    var ooption = new Array();
    var tex = new Array();
    var val = new Array();
    tex = sStrText.split('|');
    val = sStrValue.split('|');
    if (tex.length != val.length) {
        alert("初始化字段错误！！！");
        return;
    }
    while (oSelect.options.length != 0) {
        for (var i = 0; i < oSelect.options.length; i++)
            oSelect.options.remove(i);
        oSelect.options.length = oSelect.options.length / 2;
    }
    for (var i = 0; i < val.length; i++) {
        ooption[i] = document.createElement("option");
        ooption[i].text = tex[i];
        ooption[i].value = val[i];
        oSelect.add(ooption[i]);
    }
}
function StringToSelect2(sStrText, sStrValue, oSelect) {
    if (typeof (oSelect) != "object" || typeof (sStrText) != "string" || typeof (sStrValue) != "string")
        return;
    if (oSelect.tagName != "SELECT")
        return;
    var ooption = new Array();
    var tex = new Array();
    var val = new Array();
    tex = sStrText.split('|');
    val = sStrValue.split('|');
    if (tex.length != val.length) {
        alert("初始化字段错误！！！");
        return;
    }
    while (oSelect.options.length != 0) {
        for (var i = 0; i < oSelect.options.length; i++)
            oSelect.options.remove(i);
        oSelect.options.length = oSelect.options.length / 2;
    }
    for (var i = 0; i < val.length; i++) {
        ooption[i] = document.createElement("option");
        ooption[i].text = tex[i];
        ooption[i].value = val[i];
        oSelect.add(ooption[i]);
    }
}
//获得当前计算机时间：返回值-iFormat=1 'YYYY-MM-DD HH24:MI';iFormat=2 'YYYY-MM-DD'
function now(iFormat) {
    var year = (new Date()).getFullYear().toString();
    var month = ((new Date()).getMonth() + 1).toString();
    var day = (new Date()).getDate().toString();
    var hour = (new Date()).getHours().toString();
    var minute = (new Date()).getMinutes().toString();
    month = (month.length == 1) ? "0" + month : month;
    day = (day.length == 1) ? "0" + day : day;
    hour = (hour.length == 1) ? "0" + hour : hour;
    minute = (minute.length == 1) ? "0" + minute : minute;
    if (iFormat == "YYYY-MM-DD HH24:MI")
        return year + "-" + month + "-" + day + " " + hour + ":" + minute;
    else if (iFormat == "YYYY-MM-DD")
        return year + "-" + month + "-" + day;
    else
        return year + "-" + month + "-" + day + " " + hour + ":" + minute;
}
function goto(o) {
    window.open(o, "_self", "")
}
function ddd(t) {
    showModalDialog(t, window, "status:no;center:yes;help:no;minimize:no;maximize:no;border:thick;statusbar:no;dialogWidth:240px;scroll:auto;dialogHeight:400px");
}
function showDia(sWeb, iHei, iWid) {
    var oDia = showModalDialog(sWeb, window, 'status:no;center:yes;help:no;minimize:no;maximize:no;border:thick;statusbar:no;scroll:auto;dialogHeight:' + iHei + 'px;dialogWidth:' + iWid + 'px');
    return oDia;
}
function showDia2(sWeb, iHei, iWid) {
    var oDia = showModelessDialog(sWeb, window, 'status:no;center:yes;help:no;minimize:no;maximize:no;border:thick;statusbar:no;scroll:auto;dialogHeight:' + iHei + 'px;dialogWidth:' + iWid + 'px');
    return oDia;
}
function prop(o) {
    newWin = window.open("", "");
    tmp = o;
    for (var prop in tmp) {
        newWin.document.writeln(prop + "=" + tmp[prop] + "<br>");
    }
}
//将日期字符传'YYYY-MM-DD HH24-MI'替换为'YYYY-MM-DD HH24:MI'
function dedDate(caDate) {
    if (caDate.length == 0) return;
    caTmp1 = caDate.slice(0, -3);

    caTmp2 = caDate.slice(-2, caDate.length);
    caTmp = caTmp1 + ":" + caTmp2;
    return caTmp;
}
//时间对话框
function timeDia(obj) {
    show_calendar('form1.' + obj)
}
//选种select,item 是value的值
function selSelect(sel, item) {
    var i;
    if (typeof (sel) != "object" || sel.tagName != "SELECT")
        return;
    for (i = 0; i < sel.options.length; i++) {
        if (sel.options[i].value == item) {
            sel.options[i].selected = true;
            return;
        }
    }
}
//只允许输入数字和小数点
function inpNum(obj, iInt, iDec) {
    var str = obj.value;
    var i = 0, j = 0, z = 0;
    for (i = 0; i < str.length; i++) {
        if (str.charCodeAt(i) < 45 || str.charCodeAt(i) > 58) {
            alert("请输入数字或小数点 ！");
            obj.focus();
            return 101;
        }
    }
    if (str.charCodeAt(0) == 45) {
        j = -1;
        z = 2;
    }
    else {
        j = 0;
        z = 1;
    }
    for (i = 0; i < str.length; i++, j++) {
        if (str.charCodeAt(i) == 46) {
            if (j > iInt) {
                alert("请输入" + iInt + "位整数，" + iDec + "位小数 ！");
                obj.focus();
                return 101;
            }
            break;
        }
        if ((j + 1) > iInt) {
            alert("请输入" + iInt + "位整数，" + iDec + "位小数 ！");
            obj.focus();
            return 101;
        }
    }
    if (str.length - j - z > iDec) {
        alert("请输入" + iInt + "位整数，" + iDec + "位小数 ！");
        obj.focus();
        return 101;
    }
    return 0;
}
//检验text框输入字符的位数
function inpLen(obj, len) {
    if (typeof (obj) == "undefined" && obj.tagName != "TEXT")
        return;
    var str = obj.value;
    if (str.length > len) {
        alert("对不起！这里只能输入" + len + "位字符");
        obj.select();
    }

}
//去空格
function remBlank(str) {
    var i, j;
    var nLength = str.length;
    //去前面空格
    for (i = 0; i < nLength; i++) {
        if (str.charCodeAt(i) != 32) {
            str = str.slice(i, nLength);
            break;
        }
    }
    if (i == nLength) {
        str = "";
        return str;
    }
    //去后面空格
    for (i = str.length - 1; i >= 0; i--) {
        if (str.charCodeAt(i) != 32) {
            str = str.slice(0, i + 1);
            break;
        }
    }
    //如果只有一个空格
    if (str.length == 1 && str.charCodeAt(0) == 32)
        str = "";
    return str;
}
//转换代码与字符串
function codeToStr(strCode, strChar, tbl, col) {
    var aStr = new Array();
    var aCode = new Array();
    aStr = strChar.split('|');
    aCode = strCode.split('|');

    for (var i = 1; i < tbl.rows.length - 1; i++) {
        code = remBlank(tbl.rows[i].cells[col].innerText);
        for (var j = 0; j < aCode.length; j++) {
            if (code == aCode[j]) {
                tbl.rows[i].cells[col].innerText = " " + aStr[j];
                break;
            }
        }
    }
}
//将已'|'分开的币值转换
function codeToStr5(strCode, strChar, tbl, col) {
    var aStr = new Array();
    var aCode = new Array();
    aStr = strChar.split('|');
    aCode = strCode.split('|');
    var arValue = new Array();
    for (var i = 1; i < tbl.rows.length - 1; i++) {
        arValue = remBlank(tbl.rows[i].cells[col].innerText).split('|');
        code = arValue[1];
        for (var j = 0; j < aCode.length; j++) {
            if (code == aCode[j]) {
                tbl.rows[i].cells[col].innerText = arValue[0] + " " + aStr[j];
                break;
            }
        }
    }
}
//转换代码与字符串
function codeToStr2(strCode, strChar, str) {
    var aStr = new Array();
    var aCode = new Array();
    aStr = strChar.split('|');
    aCode = strCode.split('|');

    code = remBlank(str);
    for (var j = 0; j < aCode.length; j++) {
        if (code == aCode[j]) {
            return aStr[j];
        }
    }

}

//爷面按回车后焦点按参数顺序切换
function chaFocus() {
    var i;
    if (event.keyCode != 13) return;
    if (typeof (document.activeElement) == "undefined")
        eval("document.all." + arguments[0]).focus();
    //按顺序聚焦
    for (i = 0; i < arguments.length; i++) {
        if (arguments[i] == document.activeElement.name) {
            i++;
            iCur = (i == arguments.length) ? 0 : i;
            var o = eval("document.all." + arguments[iCur]);
            if (typeof (o) == "object") {
                o.focus();
                bFlag = '1';
            }
            return;
        }
    }
    if (i == arguments.length)
        eval("document.all." + arguments[0]).focus();
    return 0;
}
//打开源文件
function openSrc() {
    if (event.shiftKey == true && event.ctrlKey == true && event.altKey == true) {
        window.location = "view-source:" + window.location.href;
    }
}
//格式化金额
function formatCurrency(num, cur) {
    var sSymbol = "";
    num = remBlank(num);
    if (num.toString().charCodeAt(0) == 45) {
        num = num.toString().slice(1, num.length);
        sSymbol = "-";
    }
    num = num.toString().replace(/re,/g, '');
    if (isNaN(num)) {
        num = "0";
        //		document.currencyform.input3.value="";
    }
    cents = Math.floor((num * 100 + 0.5) % 100);
    num = Math.floor(num).toString();
    if (cents < 10)
        cents = "0" + cents;
    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3) ; i++)
        num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3));
    return (sSymbol + cur + num + '.' + cents);
}
function formatNum(num) {
    var sSymbol = "";
    num = remBlank(num);
    if (num.toString().charCodeAt(0) == 45) {
        num = num.toString().slice(1, num.length);
        sSymbol = "-";
    }
    num = num.toString().replace(/re,/g, '');
    if (isNaN(num)) {
        num = "0";
        //		document.currencyform.input3.value="";
    }
    cents = Math.floor((num * 100 + 0.5) % 100);
    num = Math.floor(num).toString();
    if (cents < 10)
        cents = "0" + cents;
    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3) ; i++)
        num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3));
    if (cents == "00")
        return (sSymbol + num + '.00');
    else
        return (sSymbol + num + '.' + cents);
}
function recoverCurrency(str) {
    if (remBlank(str).length == 0)
        return 0;
    return str.replace(/,/g, '');
}
//插入超连接 sTbl:Table's name;nCol:Table colum number;sWeb:link webPage;nNum:alter variable number;sDip:display name
//替换参数用' &i&'
function insLink(sTbl, nCol, sWeb, nNum, sDip) {
    var tbl = eval("document.all." + sTbl);
    var td, pattern, val;
    var nLength = tbl.rows.length - 1;
    for (var i = 1; i < nLength; i++) {
        bFlag = 0;
        if (typeof (tbl.rows[i].cells[nCol]) == "object") {
            td = tbl.rows[i].cells[nCol];
            sWebAlter = sWeb;
            for (var j = 1; j <= parseInt(nNum) ; j++) {
                pattern = new RegExp("&" + j + "&");
                val = eval("td.val" + j);
                if (remBlank(val) == "") {
                    bFlag = 1;
                    break;
                }
                sWebAlter = sWebAlter.replace(pattern, val);
            }
            if (bFlag == 0)
                td.innerHTML = "&nbsp;" + "<a href='javascript:' onclick=creFrame2('" + sWebAlter + "','" + sDip + "',top.document.all.select1)>" + td.innerText + "</a>" + "&nbsp;";
        }
    }
    return 1;
}
function insLink4(sTbl, nCol, sWeb, nNum, sDip) {
    var tbl = eval("document.all." + sTbl);
    var td, pattern, val;
    var nLength = tbl.rows.length - 1;
    for (var i = 1; i < nLength; i++) {
        bFlag = 0
        if (typeof (tbl.rows[i].cells[nCol]) == "object") {
            td = tbl.rows[i].cells[nCol];
            sWebAlter = sWeb;
            for (var j = 1; j <= parseInt(nNum) ; j++) {
                pattern = new RegExp("&" + j + "&");
                val = eval("td.val" + j);
                if (remBlank(val) == "")
                    if (remBlank(val) == "") {
                        bFlag = 1;
                        break;
                    }
                sWebAlter = sWebAlter.replace(pattern, val);
            }
            if (bFlag == 0)
                td.innerHTML = "&nbsp;" + "<a href='' onclick=window.close();window.dialogArguments.parent.parent.creFrame2('" + sWebAlter + "','" + sDip + "',top.document.all.select1);event.returnValue=false;>" + td.innerText + "</a>" + "&nbsp;";
        }
    }
    return 1;
}
//open window
function insLink2(sTbl, nCol, sWeb, nNum, nWidth, nHeight) {
    var tbl = eval("document.all." + sTbl);
    var td, pattern, val;
    var nLength = tbl.rows.length - 1;
    for (var i = 1; i < nLength; i++) {
        bFlag = 0;
        if (typeof (tbl.rows[i].cells[nCol]) == "object") {
            td = tbl.rows[i].cells[nCol];
            sWebAlter = sWeb;
            for (var j = 1; j <= parseInt(nNum) ; j++) {
                pattern = new RegExp("&" + j + "&");
                val = eval("td.val" + j);
                if (remBlank(val) == "")
                    if (remBlank(val) == "") {
                        bFlag = 1;
                        break;
                    }
                sWebAlter = sWebAlter.replace(pattern, val);
            }
            if (bFlag == 0)
                td.innerHTML = "&nbsp;" + "<a href='javascript:' onclick=showDia('" + sWebAlter + "'," + nHeight + "," + nWidth + ")>" + td.innerText + "</a>" + "&nbsp;";
        }
    }
    return 1;
}
//mail to
function insLink3(sTbl, nCol, sWeb, nNum) {
    var tbl = eval("document.all." + sTbl);
    var td, pattern, val;
    var nLength = tbl.rows.length - 1;
    for (var i = 1; i < nLength; i++) {
        bFlag = 0;
        if (typeof (tbl.rows[i].cells[nCol]) == "object") {
            td = tbl.rows[i].cells[nCol];
            sWebAlter = sWeb;
            for (var j = 1; j <= parseInt(nNum) ; j++) {
                pattern = new RegExp("&" + j + "&");
                val = eval("td.val" + j);
                if (remBlank(val) == "")
                    if (remBlank(val) == "") {
                        bFlag = 1;
                        break;
                    }
                sWebAlter = sWebAlter.replace(pattern, val);
            }
            if (bFlag == 0)
                td.innerHTML = "&nbsp;" + "<a href='javascript:' onclick=location='mailto:" + sWebAlter + "'>" + td.innerText + "</a>" + "&nbsp;";
        }
    }
    return 1;
}
function insLink5(sTbl, nCol, sWeb, nNum) {
    var tbl = eval("document.all." + sTbl);
    var td, pattern, val;
    var nLength = tbl.rows.length - 1;
    for (var i = 1; i < nLength; i++) {
        bFlag = 0;
        if (typeof (tbl.rows[i].cells[nCol]) == "object") {
            td = tbl.rows[i].cells[nCol];
            sWebAlter = sWeb;
            for (var j = 1; j <= parseInt(nNum) ; j++) {
                pattern = new RegExp("&" + j + "&");
                val = eval("td.val" + j);
                if (remBlank(val) == "")
                    if (remBlank(val) == "") {
                        bFlag = 1;
                        break;
                    }
                sWebAlter = sWebAlter.replace(pattern, val);
            }
            if (bFlag == 0)
                td.innerHTML = "&nbsp;" + "<a href='javascript:' onclick=window.close();window.dialogArguments.parent.parent.location='mailto:" + sWebAlter + "';event.returnValue=false;>" + td.innerText + "</a>" + "&nbsp;";
        }
    }
    return 1;
}
//初始化Text,button组合控件
var bFlag = '0';//used to judge the focus src ,from keydown or click
function initTexBut(sText) {
    var oText = eval("document.all." + sText);
    var oBut = eval("document.all." + sText + "_but");
    oText.onfocus = new Function("eval('document.all." + sText + "_but').style.display='';if(bFlag=='1'){bFlag='0';eval('document.all." + sText + "_but').click();}");
    oText.onblur = new Function("if(document.activeElement.name!='" + sText + "_but')eval('document.all." + sText + "_but').style.display='none'");
    oBut.onblur = new Function("if(document.activeElement.name!='" + sText + "')eval('document.all." + sText + "_but').style.display='none'");

}
//初始化Remark框
function disRemark(sRem) {
    var oRem = eval("document.all." + sRem);
    oRem.onfocus = new Function("focRemark('" + sRem + "')");
}
function disRemark2(sRem) {
    var oRem = eval("document.all." + sRem);
    oRem.onclick = new Function("focRemark2('" + sRem + "')");
}
var bFlagRem = 0;
function focRemark(sRem) {
    if (bFlagRem == 1) return;
    var oRem = eval("document.all." + sRem);
    if (oRem.value.length != 0)
        document.all.texttt.innerText = oRem.value;
    document.all.remark.style.display = "block";
    document.all.remark.flag = sRem;
    document.all.texttt.focus();
}
function focRemarkTD(oRem) {
    if (bFlagRem == 1) return;
    if (remBlank(oRem.innerText).length != 0)
        document.all.texttt.innerText = oRem.innerText;
    document.all.remark.style.display = "block";
    document.all.remark.flag = oRem;
    document.all.texttt.focus();
}
function bluRemarkTD(oObj) {
    //	document.all.texttt.onchange="";	
    var oRem = oObj.parentNode.flag;
    oRem.innerText = " " + remBlank(document.all.texttt.innerText);
    document.all.texttt.innerText = "";
    document.all.remark.style.display = "none";
    bFlagRem = 1;
    oRem.focus();
    oRem.onblur = new Function("bFlagRem=0");
    //	oRem.onkeydown=new Function("return false");
}
function focRemark2(sRem) {
    if (bFlagRem == 1) return;
    var oRem = eval("document.all." + sRem);
    if (oRem.value.length != 0) {
        document.all.texttt2.innerText = oRem.value;
        document.all.hiddennn.value = (oRem.value);
    }
    document.all.remark2.style.display = "block";
    document.all.remark2.flag = sRem;
    document.all.texttt2.focus();
}
function bluRemark2(oObj) {

    var oRem = eval("document.all." + oObj.parentNode.flag);
    if (remBlank(document.all.texttt2.innerText) != remBlank(document.all.hiddennn.value)) {
        if (typeof (eval("document.all." + oRem.name + "_id")) != "undefined")
            eval("document.all." + oRem.name + "_id").value = "";
    }
    oRem.value = document.all.texttt2.innerText;
    document.all.texttt2.innerText = "";
    document.all.hiddennn.value = "";
    document.all.remark2.style.display = "none";
    bFlagRem = 1;
    oRem.focus();
    oRem.onblur = new Function("bFlagRem=0");
    //	oRem.onkeydown=new Function("return false");
}
function bluRemark(oObj) {
    //	document.all.texttt.onchange="";	
    var oRem = eval("document.all." + oObj.parentNode.flag);
    oRem.value = document.all.texttt.innerText;
    document.all.texttt.innerText = "";
    document.all.remark.style.display = "none";
    bFlagRem = 1;
    oRem.focus();
    oRem.onblur = new Function("bFlagRem=0");
    //	oRem.onkeydown=new Function("return false");
}

function clickESC() {
    if (event.keyCode == "27") {
        if (typeof (document.all.remark) != "undefined") {
            document.all.remark.style.display = "none";
        }
        if (typeof (document.all.remark2) != "undefined") {
            document.all.remark2.style.display = "none";
        }
    }
}
function clickDiaESC() {
    if (event.keyCode == "27") {
        if (typeof (document.all.remark) != "undefined") {
            if (document.all.remark.style.display != "none")
                document.all.remark.style.display = "none";
        }
        else {
            self.close();
        }

    }
}
//全选
var bSelCheAll = 0;
function selCheAll(sTbl, nCol) {
    oTbl = eval("document.all." + sTbl);
    oTbl.rows[0].cells[nCol].innerHTML = "<input type='checkbox' onclick=cheAll('" + sTbl + "','" + nCol + "')>"
}
function selCheAll3(sTbl, nCol) {
    oTbl = eval("document.all." + sTbl);
    oTbl.rows[0].cells[nCol].innerHTML = "<input type='checkbox' onclick=cheAll2('" + sTbl + "','" + nCol + "')>"
}
function selCheAll2(sTbl, nCol) {
    oTbl = eval("document.all." + sTbl + "_head");
    oTbl.rows[0].cells[nCol].innerHTML = "<input type='checkbox' onclick=cheAll('" + sTbl + "','" + nCol + "')>"
}
function cheAll(sTbl, nCol) {
    oTbl = eval("document.all." + sTbl);
    if (bSelCheAll == 0) {
        for (var i = 1; i < oTbl.rows.length - 1; i++) {
            if (oTbl.rows[i].style.display != "none")
                oTbl.rows[i].cells[nCol].childNodes[0].checked = 1;
        }
        bSelCheAll = 1;
    }
    else if (bSelCheAll == 1) {
        for (var i = 1; i < oTbl.rows.length - 1; i++) {
            if (oTbl.rows[i].style.display != "none")
                oTbl.rows[i].cells[nCol].childNodes[0].checked = 0;
        }
        bSelCheAll = 0;
    }
    return true;
}
function cheAll2(sTbl, nCol) {
    oTbl = eval("document.all." + sTbl);
    if (bSelCheAll == 0) {
        for (var i = 1; i < oTbl.rows.length; i++) {
            oTbl.rows[i].cells[nCol].childNodes[0].checked = 1;
        }
        bSelCheAll = 1;
    }
    else if (bSelCheAll == 1) {
        for (var i = 1; i < oTbl.rows.length; i++) {
            oTbl.rows[i].cells[nCol].childNodes[0].checked = 0;
        }
        bSelCheAll = 0;
    }
    return true;
}
//金额转大写
function chaAmoReg(sAmount) {
    var sBig = "零壹贰叁肆伍陆柒捌玖";
    var sResult = "";
    var bFlag = 0;
    //检验参数是否为合法数字，取小数点后两位，四舍五入。
    var sAmo = new Array();
    var sPre = "";
    sAmount = remBlank(sAmount);
    if (sAmount.slice(0, 1) == '-') {
        sAmount = sAmount.slice(1, sAmount.length);
        sPre = "负";
    }
    var fAmount = parseFloat(sAmount);
    if (isNaN(fAmount)) return;
    sAmo = (Math.round(fAmount * 100) / 100).toString().split(".");
    if (sAmo[0] != "0") {
        for (var i = 0, j = sAmo[0].length; i < sAmo[0].length; i++, j--) {
            if (sAmo[0].charAt(i) != "0") {
                sResult += sBig.charAt(sAmo[0].charAt(i));
                sResult += getPos(j);
            }
            else {
                for (var z = 1; i < sAmo[0].length; z++, i++, j--) {
                    if (j == 1) {
                        sResult += "元"
                        break;
                    }
                    if (j == 5) {
                        sResult += "万"
                        break;
                    }
                    if (j == 9) {
                        sResult += "亿"
                        break;
                    }
                    if (sAmo[0].charAt(i + 1) != "0") {
                        sResult += "零";
                        break;
                    }
                }
            }
        }
    }
    if (typeof (sAmo[1]) != "undefined") {
        for (var i = 0, j = -1; i < sAmo[1].length; i++, j--) {
            sResult += sBig.charAt(sAmo[1].charAt(i));
            if (sAmo[1].charAt(i) != "0")
                sResult += getPos(j);
        }
    }
    else {
        sResult += "整";
    }
    return sPre + sResult;
}
function getPos(nPos) {
    var sPos = "";
    switch (nPos) {
        case -1:
            sPos = "角";
            break;
        case -2:
            sPos = "分";
            break;
        case 1:
            sPos = "元";
            break;
        case 2:
            sPos = "拾";
            break;
        case 3:
            sPos = "佰";
            break;
        case 4:
            sPos = "仟";
            break;
        case 5:
            sPos = "万";
            break;
        case 6:
            sPos = "拾";
            break;
        case 7:
            sPos = "佰";
            break;
        case 8:
            sPos = "仟";
            break;
        case 9:
            sPos = "亿";
            break;
        case 10:
            sPos = "拾";
            break;
        case 11:
            sPos = "佰";
            break;
        case 12:
            sPos = "仟";
            break;
    }
    return sPos;
}
//检验页面是否输入完整,return false 未输入完整
function cheText() {
    for (var i = 0; i < arguments.length / 2; i++) {
        if (remBlank(eval("document.all." + arguments[2 * i])).value.length == 0) {
            alert("请输入" + arguments[2 * i + 1]);
            eval("document.all." + arguments[2 * i]).focus();
            return false;
        }
    }
    return true;
}
//交换表的两列
function swpTblCol(sTbl, nCol1, nCol2) {
    oTbl = eval("document.all." + sTbl);
    //	oTbl.rows[0].cells[nCol1].swapNode(oTbl.rows[0].cells[nCol2]);
    sTmp = oTbl.rows[0].cells[nCol1].innerText;
    oTbl.rows[0].cells[nCol1].innerText = oTbl.rows[0].cells[nCol2].innerText;
    oTbl.rows[0].cells[nCol2].innerText = sTmp;
    sTmp = oTbl.rows[0].cells[nCol1].flag;
    oTbl.rows[0].cells[nCol1].flag = oTbl.rows[0].cells[nCol2].flag;
    oTbl.rows[0].cells[nCol2].flag = sTmp;
    sTmp = oTbl.rows[0].cells[nCol1].width;
    oTbl.rows[0].cells[nCol1].width = oTbl.rows[0].cells[nCol2].width;
    oTbl.rows[0].cells[nCol2].width = sTmp;
    var nLength = oTbl.rows.length - 2;
    for (var i = 1; i < nLength; i++) {
        //		if(typeof(oTbl.rows[i].cells[nCol1])!="undefined"&&typeof(oTbl.rows[i].cells[nCol2])!="undefined")
        {
            sTmp = oTbl.rows[i].cells[nCol1].innerText;
            oTbl.rows[i].cells[nCol1].innerText = oTbl.rows[i].cells[nCol2].innerText;
            oTbl.rows[i].cells[nCol2].innerText = sTmp;
        }
    }
}
//隐藏表的某列
function hidTblCol(sTbl, nCol) {
    oTbl = eval("document.all." + sTbl);
    oTbl.style.tableLayout = "fixed";
    oTbl.rows[0].cells[nCol].style.width = "0px"
    /*	for(var i=0;i<oTbl.rows.length;i++)
        {
            if(typeof(oTbl.rows[i].cells[nCol])!="undefined")
                oTbl.rows[i].cells[nCol].style.display='none';
        }*/
}
//显示表的某列
function disTblCol(sTbl, nCol) {
    oTbl = eval("document.all." + sTbl);
    for (var i = 0; i < oTbl.rows.length; i++) {
        if (typeof (oTbl.rows[i].cells[nCol]) != "undefined")
            oTbl.rows[i].cells[nCol].style.display = 'block';
    }
}
//on keydown inspect is number
function testNum() {
    if ((event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105) && event.keyCode != 8 && event.keyCode != 46 && (event.keyCode < 37 || event.keyCode > 40) && event.keyCode != 13)
        event.returnValue = false;
}
//get element's position
function getElementPosition(obj, direction) {
    objPosition = 0;
    while (obj.tagName != "BODY") {
        objPosition += obj["offset" + direction];
        obj = obj.offsetParent;
        if (typeof (obj) == "undefined") break;
    }
    return objPosition;
}
//
function resizeDiv() {
    var aCols = new Array();
    aCols = top.document.wl.document.all.forum.cols.split(",");
    //	alert(aCols);
    var oTbl = document.all.DataTable_head;
    var oTbl2 = document.all.DataTable;
    if (aCols[0] == "21") {
        oTbl.rows[0].cells[0].width = "17";
        oTbl2.rows[0].cells[0].width = "17";
    }
    else {
        oTbl.rows[0].cells[0].width = "24";
        oTbl2.rows[0].cells[0].width = "24";
    }

}
//获取中文字母,还不行
function getPYCode(str,pyname) {
    var result = "";
    for (var i = 0; i < str.length; i++) {
        result += getPY(str.charAt(i).toString())
    }
    document.getElementById(pyname).innerHTML = result
}
function getPY(s) {
    if (s != "") {
        //execScript("tmp=asc(\"" + s + "\")", "vbscript");
        var tmp = s.charCodeAt();

        //alert(tmp);
        //tmp = 65536 + tmp;
        var py = "";
        if (tmp >= 11055 && tmp <= 11846) {
            py = "A"
        } else if (tmp >= 11847 && tmp <= 12555) {
            py = "B"
        } else if (tmp >= 12556 && tmp <= 12837) {
            py = "C"
        } else if (tmp >= 12838 && tmp <= 13317) {
            py = "D"
        } else if (tmp >= 13318 && tmp <= 14089) {
            py = "E"
        } else if (tmp >= 14090 && tmp <= 14148) {
            py = "F"
        } else if (tmp >= 14149 && tmp <= 14629) {
            py = "G"
        } else if (tmp >= 14630 && tmp <= 14913) {
            py = "H"
        } else if (tmp >= 14914 && tmp <= 14921) {
            py = "J"
        } else if (tmp >= 14922 && tmp <= 15164) {
            py = "K"
        } else if (tmp >= 15165 && tmp <= 15639) {
            py = "L"
        } else if (tmp >= 15640 && tmp <= 16211) {
            py = "M"
        } else if (tmp >= 16212 && tmp <= 16473) {
            py = "N"
        } else if (tmp >= 16474 && tmp <= 17416) {
            py = "O"
        } else if (tmp >= 17417 && tmp <= 17921) {
            py = "P"
        } else if (tmp >= 17922 && tmp <= 18238) {
            py = "Q"
        } else if (tmp >= 18239 && tmp <= 18525) {
            py = "R"
        } else if (tmp >= 18526 && tmp <= 18709) {
            py = "S"
        } else if (tmp >= 18710 && tmp <= 19217) {
            py = "T"
        } else if (tmp >= 19218 && tmp <= 19774) {
            py = "W"
        } else if (tmp >= 19775 && tmp <= 20282) {
            py = "X"
        } else if (tmp >= 20283 && tmp <= 20318) {
            py = "Y"
        } else if (tmp >= 20319 && tmp <= 62289) {
            py = "Z"
        } else {
            py = s.charAt(0)
        }

        //if (tmp >= 45217 && tmp <= 45252) {
        //    py = "A"
        //} else if (tmp >= 45253 && tmp <= 45760) {
        //    py = "B"
        //} else if (tmp >= 45761 && tmp <= 46317) {
        //    py = "C"
        //} else if (tmp >= 46318 && tmp <= 46825) {
        //    py = "D"
        //} else if (tmp >= 46826 && tmp <= 47009) {
        //    py = "E"
        //} else if (tmp >= 47010 && tmp <= 47296) {
        //    py = "F"
        //} else if ((tmp >= 47297 && tmp <= 47613) || (tmp == 63193)) {
        //    py = "G"
        //} else if (tmp >= 47614 && tmp <= 48118) {
        //    py = "H"
        //} else if (tmp >= 48119 && tmp <= 49061) {
        //    py = "J"
        //} else if (tmp >= 49062 && tmp <= 49323) {
        //    py = "K"
        //} else if (tmp >= 49324 && tmp <= 49895) {
        //    py = "L"
        //} else if (tmp >= 49896 && tmp <= 50370) {
        //    py = "M"
        //} else if (tmp >= 50371 && tmp <= 50613) {
        //    py = "N"
        //} else if (tmp >= 50614 && tmp <= 50621) {
        //    py = "O"
        //} else if (tmp >= 50622 && tmp <= 50905) {
        //    py = "P"
        //} else if (tmp >= 50906 && tmp <= 51386) {
        //    py = "Q"
        //} else if (tmp >= 51387 && tmp <= 51445) {
        //    py = "R"
        //} else if (tmp >= 51446 && tmp <= 52217) {
        //    py = "S"
        //} else if (tmp >= 52218 && tmp <= 52697) {
        //    py = "T"
        //} else if (tmp >= 52698 && tmp <= 52979) {
        //    py = "W"
        //} else if (tmp >= 52980 && tmp <= 53688) {
        //    py = "X"
        //} else if (tmp >= 53689 && tmp <= 54480) {
        //    py = "Y"
        //} else if (tmp >= 54481 && tmp <= 62289) {
        //    py = "Z"
        //} else {
        //    py = s.charAt(0)
        //}
        return py
    }
}