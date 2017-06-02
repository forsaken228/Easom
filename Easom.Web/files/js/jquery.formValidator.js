/*
* jQuery FromValidator ver 0.2
*
* Copyright (c) 2009 loskiv@gmail.com
* 
*/
//var options={
//    must:true,
//    rule :'userName','num','date','datetime','email','url','ip','mobile','tel','post','pid','range','compare','len','reg','select','radiobox','checkbox','radioboxs','checkboxs'
//    controlID :'txtSurePassword',
//    compareID:'txtPassword',//要比较的源ID
//    compareType:'==', //比较方式： ==,>=,<=,<,>  
//    unionID :'chkMan', //要进行关联验证的ID，当值不为空时，开启验证；当unionMode为:checkboxs或radioboxs时，该值为name值
//    unionValue  :'==',//当且仅当unionID的value与其相等时，才进行验证。
//    notUnionValue  :'==',//当且仅当unionID的value与其不相等时，才进行验证。
//    unionMode:'checkbox','checkboxs','radiobox','radioboxs','select' //当unionID不为空时有效， 该值为空，说明可按value取值。
//    min:10,//当rule为：range时，进行比较，起始值。
//    max:20,//当rule为：range时，进行比较，最大值。
//    length:8//当rule为：len时,验证长度。
//    msg:'请输入用户名！'。
//    mustMsg:'密码不能为空！'。
//    regFormat:/^[a-zA-Z0-9]+$/
//    errFormat:'非法的数据格式' //当rule为：compare,range时有效
//    minLen:6 //当rule为len时，该值是最小长度；
//    maxLen:20 //当rule为len时，该值为最大长度；
//    elementValue:-1,//当rule为select时，与该值做比较，如果等于该值，则验证无效，通常为非值。
//    ajaxURL:'CheckUserName.aspx' //当rule为userName时该值有效,异步验证用户名是否有效
//    ajaxValue:'1' //当rule为userName时该值与ajax返回的值相同时，验证通过
//    ajaxData:'userName='//当rule为userName时该值有效。
//    ajaxMsg:'已经存在的用户名，请更换用户名重试。'
//    validateMode:验证模式，[break]|[continue];如果为break，验证未通过时，将停止验证。
//    isShowErrrorMsg：是否显示错误提示信息，如果设置为flash，则只改变样式
//}

(function ($) {

    $.extend($.fn, {
        formValidator: {
            defaults: {
                preFix: "for-",
                normalClass: '',
                okClass: "has-success",
                errorClass: "has-error",
                txtNormal: "txt",
                txtCur: "cur",
                txtError: "has-error",
                formID: null,
                options: null,
                ajaxPass: true,
                validateMode: "break",
                isShowErrrorMsg: true,
                notifyContainer: null
            },
            onPassCallBack: null,
            setSubmitButtonState: function (isEnable) {
                if (isEnable) {
                     $(".btn-primary").removeAttr("disabled");
                } else {
                   $(".btn-primary").attr("disabled", true);
                }
            },
            init: function (formID) {
                var _this = this;
                _this.defaults.formID = formID;
                $.each($(formID + " input"), function () {
                    if (this.type == 'text' || this.type == 'password') {
                        $(this).bind("focus", function () {
                            _this.regFocus(formID, this);
                        });
                    }
                });
                $.each($(formID + " textArea"), function () {
                    $(this).bind("focus", function () {
                        _this.regFocus(formID, this);
                    });
                });
                $(formID).submit(function () {
                    var isPass = _this.preSubmitValidator();
                    if (isPass) {

                        _this.setSubmitButtonState(false);
                        if (_this.onPassCallBack) {
                            //FireFox 下对修改FCKeditor值不能正常保存的问题
                            for (i = 0; i < window.frames.length; i++) {
                                if (window.frames[i].FCK) {
                                    window.frames[i].FCK.UpdateLinkedField();
                                }
                            }
                            _this.onPassCallBack();
                            return false;
                        }
                    }
                    //alert("_this.defaults.ajaxPass :"+_this.defaults.ajaxPass); 
                    if (!_this.defaults.ajaxPass) { return _this.defaults.ajaxPass; }
                    return isPass;
                });
            },
            regFocus: function (formID, obj) {
                var defaultSets = this.defaults;
                $(formID + " :text").removeClass(defaultSets.txtCur);
                $(formID + " :password").removeClass(defaultSets.txtCur);
                $(formID + " textArea").removeClass(defaultSets.txtCur);
                $(obj).addClass(defaultSets.txtCur);
            },
            notify: function (message) {
                var defaultSets = this.defaults;
                if (defaultSets.notifyContainer) {
                    defaultSets.notifyContainer.close();
                }
                defaultSets.notifyContainer = $().pageUtility.createNotify("growl-danger", false, "错误提示：", message);
            },
            closeNotify: function () {
                var defaultSets = this.defaults;
                if (defaultSets.notifyContainer) {
                    defaultSets.notifyContainer.close();
                }
            },
            regBlur: function (curOptions, isError, msg) {
                var defaultSets = this.defaults;
                var _this = this;
                if (isError) {
                    $('#' + curOptions.controlID).parent("div").addClass(defaultSets.txtError);
                    if (!defaultSets.isShowErrrorMsg) { return; }
                    if (msg) {
                        _this.notify(msg);
                        return;
                    }
                    if (curOptions.must) {
                        if ($("#" + curOptions.controlID).attr("value")) {
                            _this.notify(curOptions.msg);
                        } else {
                            _this.notify(curOptions.mustMsg);
                        }
                    } else {
                        _this.notify(curOptions.mustMsg);
                    }
                } else {
                    $('#' + curOptions.controlID).parent("div").removeClass(defaultSets.txtError);
                    $('#' + curOptions.controlID).parent("div").removeClass(defaultSets.txtCur);
                    if (!defaultSets.isShowErrrorMsg) { return; }
                    $('#' + defaultSets.preFix + curOptions.controlID).removeClass().addClass(defaultSets.okClass).html("&nbsp;");
                    _this.closeNotify();
                }
            },
            regValidator: function (source, regFormat) {
                var result = source.match(regFormat);
                if (result == null) return false;
                else return true;
            },
            addValidator: function (options, isCallBack) {
                if (!options) {
                    return true;
                }
                var _this = this;
                var isPass = true;
                if (!isCallBack) {
                    _this.defaults.options = options;
                }
                $.each(options, function () {
                    var curOption = this;
                    var controlObj = $("#" + this.controlID);
                    if (!isCallBack) {
                        controlObj.bind("blur", function () {
                            _this.validator(curOption, false);
                        });
                        if (this.mutiCheck) {
                            if (this.mutiCheck == 'change') {
                                controlObj.bind("change", function () {
                                    _this.validator(curOption, false);
                                });
                            }
                        }
                    } else {
                        if (!_this.validator(curOption, true)) {
                            isPass = false;
                            if (_this.defaults.validateMode == "break") {
                                controlObj.focus();
                                return isPass;
                            }
                        };
                    }
                });
                return isPass;
                //alert('reg blur ok, and the bak length:'+_this.defaults.options.length); 
            }, //end validator method
            preSubmitValidator: function () {
                //alert(this.defaults.formID+" had registed event: "+this.defaults.options.length);
                return this.addValidator(this.defaults.options, true);
            },
            validator: function (curOption, isCallBack) {
                var _this = this;
                if (curOption.rule == 'checkboxs' || curOption.rule == 'radioboxs') {
                    var hasChecked = false;
                    $.each($("input[name='" + curOption.controlID + "']"), function () {
                        var _thisObj = this;
                        $(this).bind("click", function () {
                            if (_thisObj.checked)
                                _this.regBlur(curOption, false);
                        });
                        if (this.checked) {
                            hasChecked = true;
                            return true;
                        }
                    });
                    if (!hasChecked) {
                        _this.regBlur(curOption, true);
                        return false;
                    } else {
                        _this.regBlur(curOption, false);
                    }
                } //end checkboxs or radiobos
                var controlValue = $("#" + curOption.controlID).attr("value");
                if (curOption.unionID) {
                    var unionObjValue = "";
                    if (!curOption.unionMode) {
                        unionObjValue = $("#" + curOption.unionID).attr("value");
                    } else {
                        if (curOption.unionMode == 'checkbox' || curOption.unionMode == 'radiobox') {
                            unionObjValue = $("#" + curOption.unionID).attr("checked");
                        }
                        if (curOption.unionMode == 'checkboxs' || curOption.unionMode == 'radioboxs') {
                            unionObjValue = $("input[name='" + curOption.unionID + "']:checked").val();
                        }
                        if (curOption.unionMode == 'select') {
                            unionObjValue = $("#" + curOption.unionID).val();
                        }
                    }
                    if (curOption.unionValue) {
                        if (unionObjValue != curOption.unionValue) {
                            return true;
                        }
                    }
                    if (curOption.notUnionValue) {
                        if (unionObjValue == curOption.notUnionValue) {
                            return true;
                        }
                    }
                } //end union validator
                if (!curOption.must) {
                    if (!controlValue) {
                        return true;
                    }
                } else {
                    if (!controlValue) {
                        _this.regBlur(curOption, true);
                        return false;
                    } else {
                        if (($.trim(controlValue) == "")) {
                            _this.regBlur(curOption, true);
                            return false;
                        }
                        if (!curOption.rule) {
                            _this.regBlur(curOption, false);
                            return true;
                        }
                    }
                }
                if (curOption.rule == 'num') {
                    if (isNaN(controlValue)) {
                        _this.regBlur(curOption, true);
                        return false;
                    } else {
                        _this.regBlur(curOption, false);
                    }
                }
                if (curOption.rule == 'userName') {
                    var reg = /^[a-zA-Z]+\w{3,14}$/;
                    if (!_this.regValidator(controlValue, reg)) {
                        _this.regBlur(curOption, true);
                        return false;
                    } else {
                        if (curOption.ajaxURL) {
                            $.ajax({
                                type: "GET",
                                url: curOption.ajaxURL,
                                data: curOption.ajaxData + controlValue + "&r=" + Math.random(),
                                success: function (msg) {
                                    //alert(msg);
                                    if (msg == curOption.ajaxValue) {
                                        _this.regBlur(curOption, false);
                                        _this.defaults.ajaxPass = true;
                                    } else {
                                        _this.regBlur(curOption, true, curOption.ajaxMsg);
                                        _this.defaults.ajaxPass = false;
                                        //alert("ajax validate is wrong : "+_this.defaults.ajaxPass);
                                    }
                                }
                            });
                        }
                        else {
                            _this.regBlur(curOption, false);
                        }
                    }
                }
                if (curOption.rule == 'date') {
                    var reg = /^(\d{4})(-|\/)(\d{1,2})\2(\d{1,2})$/;
                    if (!_this.regValidator(controlValue, reg)) {
                        _this.regBlur(curOption, true);
                        return false;
                    } else {
                        _this.regBlur(curOption, false);
                    }
                }
                if (curOption.rule == 'datetime') {
                    var reg = /^(\d{4})(-|\/)(\d{1,2})\2(\d{1,2}) (\d{1,2}):(\d{1,2}):(\d{1,2})$/;
                    if (!_this.regValidator(controlValue, reg)) {
                        _this.regBlur(curOption, true);
                        return false;
                    } else {
                        _this.regBlur(curOption, false);
                    }
                }
                //email
                if (curOption.rule == 'email') {
                    var reg = /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/;
                    if (!_this.regValidator(controlValue, reg)) {
                        _this.regBlur(curOption, true);
                        return false;
                    } else {
                        _this.regBlur(curOption, false);
                    }
                }
                //url
                if (curOption.rule == 'url') {
                    //var reg = "^[a-zA-z]+://[^\s]*$";
                    var reg = "^((https|http)://)(([0-9a-z_!~*’().&=+$%-]+: )?[0-9a-z_!~*’().&=+$%-]+@)?(([0-9]{1,3}\.){3}[0-9]{1,3}|([0-9a-z_!~*’()-]+\.)*([0-9a-z][0-9a-z-]{0,61})?[0-9a-z]\.[a-z]{2,6})(:[0-9]{1,4})?";
                    if (!_this.regValidator(controlValue, reg)) {
                        _this.regBlur(curOption, true);
                        return false;
                    } else {
                        _this.regBlur(curOption, false);
                    }
                }
                //ip
                if (curOption.rule == 'ip') {
                    var reg = /^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$/;
                    if (!_this.regValidator(controlValue, reg)) {
                        _this.regBlur(curOption, true);
                        return false;
                    } else {
                        _this.regBlur(curOption, false);
                    }
                }
                //mobile
                if (curOption.rule == 'mobile') {
                    var reg = /^1\d{10}$/;
                    if (!_this.regValidator(controlValue, reg)) {
                        _this.regBlur(curOption, true);
                        return false;
                    } else {
                        _this.regBlur(curOption, false);
                    }
                }
                //tel
                if (curOption.rule == 'tel') {
                    var reg = /^(([0\+]\d{2,3}-)?(0\d{2,3})-)?(\d{7,8})(-(\d{1,}))?$/;
                    if (!_this.regValidator(controlValue, reg)) {
                        _this.regBlur(curOption, true);
                        return false;
                    } else {
                        _this.regBlur(curOption, false);
                    }
                }
                if (curOption.rule == 'post') {
                    var reg = /^\d{6}$/;
                    if (!_this.regValidator(controlValue, reg)) {
                        _this.regBlur(curOption, true);
                        return false;
                    } else {
                        _this.regBlur(curOption, false);
                    }
                }
                if (curOption.rule == 'pid') {
                    var reg = /^(\d{15}|\d{17}[x0-9])$/;
                    if (!_this.regValidator(controlValue, reg)) {
                        _this.regBlur(curOption, true);
                        return false;
                    } else {
                        _this.regBlur(curOption, false);
                    }
                }
                if (curOption.rule == 'range') {
                    if (isNaN(controlValue)) {
                        _this.regBlur(curOption, true, curOption.errFormat);
                        return false;
                    }
                    if (!(controlValue >= curOption.min && controlValue <= curOption.max)) {
                        _this.regBlur(curOption, true);
                        return false;
                    } else {
                        _this.regBlur(curOption, false);
                    }
                }
                if (curOption.rule == 'compare') {
                    var comPareValue = $("#" + curOption.compareID).attr("value");
                    if (curOption.compareType == "==" || !curOption.compareType) {
                        if (controlValue != comPareValue) {
                            _this.regBlur(curOption, true);
                            return false;
                        } else {
                            _this.regBlur(curOption, false);
                        }
                    } else {
                        if (curOption.compareType == ">=") {
                            if (controlValue >= comPareValue) {
                                _this.regBlur(curOption, false);
                            } else {
                                _this.regBlur(curOption, true);
                                return false;
                            }
                        }
                        if (curOption.compareType == ">") {
                            if (controlValue > comPareValue) {
                                _this.regBlur(curOption, false);
                            } else {
                                _this.regBlur(curOption, true);
                                return false;
                            }
                        }
                        if (curOption.compareType == "<=") {
                            if (controlValue <= comPareValue) {
                                _this.regBlur(curOption, false);
                            } else {
                                _this.regBlur(curOption, true);
                                return false;
                            }
                        }
                        if (curOption.compareType == "<") {
                            if (controlValue < comPareValue) {
                                _this.regBlur(curOption, false);
                            } else {
                                _this.regBlur(curOption, true);
                                return false;
                            }
                        }
                        if (curOption.compareType == "!=") {
                            if (controlValue != comPareValue) {
                                _this.regBlur(curOption, true);
                            } else {
                                _this.regBlur(curOption, false);
                                return false;
                            }
                        }
                    } //end else  
                } // end compare validator
                //len
                if (curOption.rule == 'len') {
                    if (curOption.minLen && curOption.maxLen) {
                        if (controlValue.length >= curOption.minLen && controlValue.length <= curOption.maxLen) {
                            _this.regBlur(curOption, false);
                        } else {
                            _this.regBlur(curOption, true);
                            return false;
                        }
                    } else {
                        if (curOption.minLen) {
                            if (controlValue.length >= curOption.minLen) {
                                _this.regBlur(curOption, false);
                            } else {
                                _this.regBlur(curOption, true);
                                return false;
                            }
                        }
                        if (curOption.maxLen) {
                            if (controlValue.length <= curOption.minLen) {
                                _this.regBlur(curOption, false);
                            } else {
                                _this.regBlur(curOption, true);
                                return false;
                            }
                        }
                    }
                } //end len validator
                //reg
                if (curOption.rule == 'reg') {
                    var reg = curOption.regFormat;
                    if (!_this.regValidator(controlValue, reg)) {
                        _this.regBlur(curOption, true);
                        return false;
                    } else {
                        _this.regBlur(curOption, false);
                    }
                } //end
                //select
                if (curOption.rule == 'select') {
                    if (controlValue == curOption.elementValue) {
                        _this.regBlur(curOption, true);
                        return false;
                    } else {
                        _this.regBlur(curOption, false);
                    }
                } //end select
                //checkbox or radiobox 
                if (curOption.rule == 'checkbox' || curOption.rule == 'radiobox') {
                    if ($("#" + curOption.controlID).attr("checked")) {
                        _this.regBlur(curOption, false);
                    } else {
                        _this.regBlur(curOption, true);
                        return false;
                    }
                } //end checkbox or radiobo
                return true;
            } //end validator        
        }
    })
})(jQuery);