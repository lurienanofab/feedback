(function ($) {
    $.fn.feedback = function (options) {
        return this.each(function () {
            var $this = $(this);

            var opt = $.extend({}, { apiurl: "api/" }, options, $this.data());

            $(".bs-datepicker", $this).datepicker({
                autoclose: true,
                format: "mm/dd/yyyy"
            });

            $this.on("click", ".calendar-button", function (e) {
                $(this).closest(".input-group").find(".bs-datepicker").datepicker("show");
            });

            $(".feedback-criteria", $this).each(function () {
                var $feedbackCriteria = $(this);

                var now = moment();

                var currentDate = moment(new Date(now.format("MM/DD/YYYY")));

                $(".issue-date", $feedbackCriteria)
                    .datepicker("update", now.format("MM/DD/YYYY"))
                    .on("hide", function (e) {
                        var m = moment(e.date);
                        if (!m.isSame(currentDate)) {
                            currentDate = m;
                            loadUsers();
                        }
                    });

                $(".time-hour", $feedbackCriteria).val(now.format("h"));
                $(".time-minute", $feedbackCriteria).val(now.format("m"));
                $(".time-ampm", $feedbackCriteria).val(now.format("A"));

                var getSelectedUserGroup = function () {
                    return $(".user-group:checked", $feedbackCriteria).data("group");
                }

                var getSelectedDate = function () {
                    return moment($(".issue-date", $feedbackCriteria).datepicker("getDate"));
                }

                var loadUsers = function () {
                    var def = $.Deferred();

                    var group = getSelectedUserGroup();
                    var date = getSelectedDate();

                    $.ajax({
                        "url": opt.apiurl + "user/" + group + "?date=" + date.format("YYYY-MM-DD")
                    }).done(function (data) {

                        var select = $(".user", $feedbackCriteria);

                        select.html($("<option/>").val("-99").text("Unknown User"));

                        var area = "";
                        var currentGroup = null;
                        var groups = [];

                        $.each(data, function (index, item) {
                            if (area != item.AreaName) {
                                area = item.AreaName;
                                currentGroup = $("<optgroup/>", { "label": area });
                                groups.push(currentGroup);
                            }
                            currentGroup.append($("<option/>").val(item.ClientID).text(item.DisplayName));
                        });

                        select.append(groups);

                        def.resolve();
                    }).fail(function () {
                        def.reject();
                    });

                    return def.promise();
                };

                $feedbackCriteria.on("change", ".user-group", function (e) {
                    loadUsers();
                });

                loadUsers();
            });

            $(".feedback-tool", $this).each(function () {
                var $feedbackTool = $(this);

                var loadProcessTechs = function () {
                    var def = $.Deferred();

                    $.ajax({
                        "url": opt.apiurl + "tool/proctech"
                    }).done(function (data) {

                        var select = $(".proctech", $feedbackTool);

                        var lab = "";
                        var currentGroup = null;
                        var groups = [];

                        $.each(data, function (index, item) {
                            if (lab != item.LabName) {
                                lab = item.LabName;
                                currentGroup = $("<optgroup/>", { "label": lab });
                                groups.push(currentGroup);
                            }
                            currentGroup.append($("<option/>").val(item.ProcessTechID).text(item.ProcessTechName));
                        });

                        select.append(groups);

                        def.resolve();
                    }).fail(function () {
                        def.reject();
                    });

                    return def.promise();
                };

                var loadResources = function () {
                    var def = $.Deferred();

                    var procTechId = $(".proctech", $feedbackTool).val();

                    $.ajax({
                        "url": opt.apiurl + "tool/proctech/" + procTechId + "/resource"
                    }).done(function (data) {

                        $(".resource", $feedbackTool).html($.map(data, function (item, index) {
                            return $("<option/>").val(item.ResourceID).text(item.ResourceName);
                        }));

                        def.resolve();
                    }).fail(function () {
                        def.reject();
                    });

                    return def.promise();
                };

                $feedbackTool.on("change", ".tool-option", function (e) {
                    var toolOption = $(this).val();

                    if (toolOption == "none") {
                        $(".proctech", $feedbackTool).prop("disabled", true);
                        $(".resource", $feedbackTool).prop("disabled", true);
                    } else if (toolOption == "specific") {
                        $(".proctech", $feedbackTool).prop("disabled", false);
                        $(".resource", $feedbackTool).prop("disabled", false);
                    }
                }).on("change", ".proctech", function (e) {
                    loadResources();
                });

                loadProcessTechs().done(function () {
                    loadResources();
                });
            });

            $(".history-criteria", $this).each(function () {
                var $historyCriteria = $(this);

                $historyCriteria.on("change", ".date-range-preset", function (e) {
                    $historyCriteria.closest("form").submit();
                });
            });

            $(".clip", $this).each(function () {
                var $transform = $(this);

                var transformOpts = $.extend({}, { "length": 40, "suffix": "..." }, $transform.data());

                //http://stackoverflow.com/questions/1147359/how-to-decode-html-entities-using-jquery/1395954#1395954
                function decodeEntities(encodedString) {
                    var textArea = document.createElement('textarea');
                    textArea.innerHTML = encodedString;
                    return textArea.value;
                }

                var content = decodeEntities($transform.text());

                var text = $(content).text();

                console.log(text);

                var clipped = text.substring(0, transformOpts.length);
                //console.log(clipped);

                $transform.html(clipped + transformOpts.suffix).show();
            });
        });
    };
})(jQuery);