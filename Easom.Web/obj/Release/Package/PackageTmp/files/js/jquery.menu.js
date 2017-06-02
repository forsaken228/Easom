(function ($) {
    $.extend($.fn, {
        menu: {
            load: function () {
                //         <li>控制面板</li>
                //<li class="active">系统服务</li>
                var mianbaoxie = "";
                var _this = this;
                var curMatchPath = _this.getMatchPath().toUpperCase();
                var fixPre = "";
                var goOn = true;
                if (curMatchPath == '') {
                    var object = $("#sidenav li:first")
                    object.addClass('active');
                    mianbaoxie += "<li>" + object.text() + "</li>";
                    _this.setMianBaoXie(mianbaoxie);
                    goOn = false;
                }
                {
                    $.each($("#sidenav > li > a"), function (i, o) {
                        if (goOn) {
                            var objMatchPath = $(o).attr("href").toUpperCase();
                            if (curMatchPath == objMatchPath) {
                                var object = $(o).parent("li");
                                object.addClass('active');
                                mianbaoxie += "<li>" + object.text() + "</li>";
                                _this.setMianBaoXie(mianbaoxie);
                                goOn = false;
                            }
                        }
                    });
                }
                if (goOn) {
                    var changed = false;
                    var parentChange = false;
                    $.each($("#sidenav > li "), function (i, o) {
                        $.each($(o).find("ul li a"), function (j, k) {
                            if (!changed) {
                                if ($(k).attr("href").toUpperCase().indexOf(curMatchPath) >= 0) {
                                    var object = $(k).parent("li");
                                    object.addClass('active');
                                    var object2 = $(o);
                                    object2.addClass('active');
                                    object2.addClass('nav-active');
                                    object.parent("ul").attr("style", "display: block")
                                    mianbaoxie += "<li>" + object.parent().parent("li").find("a").find("span").text() + "</li>";
                                    mianbaoxie += "<li>" + object.text() + "</li>";
                                    _this.setMianBaoXie(mianbaoxie);
                                    changed = true;
                                }
                            }
                            if (changed && !parentChange) {
                                $(o).addClass("active");
                                parentChange = true;
                            }
                        });
                    });
                }
            }, //end load
            getMatchPath: function () {
                var fullPath = location.href;
                fullPath = fullPath.replace("http://", "");
                var matchPath = "";
                if (fullPath.indexOf("?") >= 0) {
                    fullPath = fullPath.split("?")[0];
                }
                if (fullPath.indexOf("/") >= 0) {
                    var pathArray = fullPath.split("/");
                    for (var i = 0; i < pathArray.length; i++) {
                        var itemPath = pathArray[i];
                        if (itemPath.length > 0) {
                            if (i > 0 && i < 3) {
                                if (itemPath.indexOf("-") < 0) {
                                    matchPath += "/" + itemPath;
                                }
                            }
                        }
                    }
                }
                return matchPath;
            },
            setMianBaoXie: function (mianbiaoxie) {
                $(".breadcrumb").append(mianbiaoxie);
            }
        }
    })
})(jQuery);
$().menu.load();