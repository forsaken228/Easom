/*
* jQuery FromValidator ver 0.2
*
* Copyright (c) 2009 loskiv@gmail.com
* 
*/
(function ($) {
    $.extend($.fn, {
        pageUtility: {
            defaults: {
                eventContainer: new Array()
            },
            addEvent: function (eventName) {
                if (eventName) {
                    var index = this.defaults.eventContainer.length;
                    this.defaults.eventContainer[index] = eventName;
                }
            },

            load: function () {
                var _eventContainer = this.defaults.eventContainer;
                $(document).ready(function () {
                    for (var i = 0; i < _eventContainer.length; i++) {
                        _eventContainer[i]();
                    }
                });
            },
            equalHeight: function (objID1, objID2) {
                var height1 = $("#" + objID1).height();
                var height2 = $("#" + objID2).height();
                if (height1 > height2) {
                    $("#" + objID2).height(height1);
                } else {
                    $("#" + objID1).height(height2);
                }
            },
            setSearchTxt: function (objId, word) {
                $("#" + objId).focus(function () {
                    this.className = "searchin";
                    if (this.value == word) {
                        this.value = "";
                    }
                });
                $("#" + objId).blur(function () {
                    if (this.value == word || !this.value) {
                        this.className = "searchout";
                        this.value = word;
                    }
                });

                if (!$("#" + objId).val()) {
                    $("#" + objId).val(word);
                } else {
                    $("#" + objId).removeClass("searchout").addClass("searchin");
                }
            },

            checkedAll: function (containerId) {
                $.each($("#" + containerId + " input:checkbox"), function (i, o) {
                    o.checked = !o.checked
                });
            },

            getChecked: function (containerId) {
                var ids = "";
                $.each($("#" + containerId + " input:checkbox"), function (i, o) {
                    if (o.checked) {
                        if (o.value != "on") {
                            ids += encodeURIComponent(o.value) + ",";
                        }
                    }
                });
                return ids;
            },

            hasChecked: function (containerId) {
                var flag = false;
                $.each($("#" + containerId + " input:checkbox"), function (i, o) {
                    if (o.checked && !flag) {
                        flag = true;
                    }
                });
                return flag;
            },
            alert: function (title, message) {
                if ($("#popID")) { $("#popID").remove(); }
                var showHtml = "<div id=\"popID\" class=\"modal-dialog\">";
                showHtml += "<div class=\"modal-content\">";
                showHtml += "<div class=\"modal-header\">";
                showHtml += "<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">&times;</button>";
                showHtml += "<h6 class=\"modal-title\">" + title + "</h6></div>";
                showHtml += "<div class=\"modal-body\">" + message + "</div>";
                showHtml += "<div class=\"modal-footer\"><button type=\"button\" class=\"btn btn-primary\" data-dismiss=\"modal\">确定</button></div>";
                showHtml += "</div></div>";
                $("#myModal").append(showHtml);
                $('#myModal').modal({
                    keyboard: true,
                    show: true
                });
            },

            loading: function (message) {

            },

            setLoading: function (message) {
                $("#loading-message").html(message);
            },

            closeLoading: function () {
                $.modal.close();
            },

            confirm: function (message, callback) {

            },

            pop: function (title, url, width, height) {
                if ($("#popID")) { $("#popID").remove(); }
                if (!width) width = 900;
                if (!height) height = 500;
                var showHtml = "<div id=\"popID\" class=\"modal-dialog modal-lg\"><div class=\"modal-content\"><div class=\"modal-header\">";
                showHtml += "<button aria-hidden=\"true\" data-dismiss=\"modal\" class=\"close\" type=\"button\">&times;</button>";
                showHtml += "<h6 class=\"modal-title\">" + title + "</h6></div>";
                //    showHtml += "<div class=\"modal-body\">";
                showHtml += "<iframe id='ifContainer' " + " width='100%' height='" + (height) + "' src='" + url + "' frameborder='0'></iframe>"
                //    showHtml += "</div>";
                //  showHtml += "<div class=\"modal-footer\"><button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\">取消</button><button type=\"button\" class=\"btn btn-primary\">确定</button></div>";
                showHtml += "</div></div>";
                $("#myModal").append(showHtml);
                $('#myModal').modal({
                    keyboard: true,
                    show: true
                });
            },
            closePop: function (callback) {
                $.modal.close();
            },

            closePop: function () {
                $.modal.close();
            },

            createNotify: function (status, isModel, title, message) {
                jQuery.gritter.add({
                    title: title,
                    text: message,
                    class_name: status,
                    image: '/files/images/screen.png',
                    sticky: isModel,
                    time: ''
                });
            },
            createPrimaryNotify: function (message) {
                this.createNotify("growl-primary", false, title, message)
            },
            createSuccessNotify: function (title, message) {
                this.createNotify("growl-success", false, title, message)
            },
            createWarningNotify: function (title, message) {
                this.createNotify("growl-warning", false, title, message)
            },
            createDangerNotify: function (title, message) {
                this.createNotify("growl-danger", false, title, message)
            },
            createInfoNotify: function (title, message) {
                this.createNotify("growl-info", false, title, message)
            },
            getFormData: function (formValues) {
                var dataString = "";
                for (var i = 0; i < formValues.length; i++) {
                    dataString = dataString + formValues[i].id + "=" + encodeURIComponent(formValues[i].value);
                    if (i != formValues.length - 1) {
                        dataString += "&";
                    }
                }
                return dataString;
            },
            copyText: function (meintext) {
                var con = meintext;
                //window.clipboardData.setData("Text",con);
                if (window.clipboardData) {
                    window.clipboardData.clearData();
                    window.clipboardData.setData("Text", con);
                    $().pageUtility.createNotify("系统提示：", "复制成功！");
                } else if (window.netscape) {     //Firefox
                    try {
                        netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
                    } catch (e) {
                        alert("被浏览器拒绝！请在浏览器地址栏输入'about:config'并回车 然后将'signed.applets.codebase_principal_support'设置为'true'");
                    }
                    var clip = Components.classes['@mozilla.org/widget/clipboard;1'].createInstance(Components.interfaces.nsIClipboard);
                    if (!clip) {
                        return;
                    }
                    var trans = Components.classes['@mozilla.org/widget/transferable;1'].createInstance(Components.interfaces.nsITransferable);
                    if (!trans) {
                        return;
                    }
                    trans.addDataFlavor('text/unicode');
                    var str = new Object();
                    var len = new Object();
                    var str = Components.classes["@mozilla.org/supports-string;1"].createInstance(Components.interfaces.nsISupportsString);
                    var copytext = con;
                    str.data = copytext;
                    trans.setTransferData("text/unicode", str, copytext.length * 2);
                    var clipid = Components.interfaces.nsIClipboard;
                    if (!clip) {
                        return false;
                    }
                    clip.setData(trans, null, clipid.kGlobalClipboard);
                    $().pageUtility.createNotify("系统提示：", "复制成功！");
                }
            } /*end copy_clip*/
        }
    })
})(jQuery);

$().pageUtility.load();