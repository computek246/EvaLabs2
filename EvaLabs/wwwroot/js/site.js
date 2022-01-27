// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {



    $("input, textarea").each(function () {
        //to add placeholder for all input that not have placeholder from label
        var $InputEl = $(this);
        var placeholder = $InputEl.attr("placeholder");
        var required = $InputEl.attr("data-val-required");
        var disabled = $InputEl.attr("disabled");
        if ((typeof placeholder === typeof undefined || placeholder === false) &&
            (typeof disabled === typeof undefined || disabled === false)) {
            $InputEl.attr("placeholder", $($InputEl.parents("div.form-group").find('label[for="' + $InputEl.attr("id") + '"]')).text());
        }
    });

    $("input[type='radio'], input[type='checkbox']").iCheck({
        checkboxClass: "icheckbox_square-blue",
        radioClass: "iradio_square-blue",
        increaseArea: "20%" // optional
    });


    $(".select2").each(function () {
        var $InputEl = $(this);
        var label = $($InputEl.parents("div.form-group").find('label[for="' + $InputEl.attr("id") + '"]')).text();
        $InputEl.select2({
            theme: "bootstrap4",
            placeholder: `Select ${label}`,
            allowClear: true
        });
    });

});

function error_handler(e) {
    if (e.errors) {
        var message = "Errors:\n";
        $.each(e.errors, function (key, value) {
            if ("errors" in value) {
                $.each(value.errors, function () {
                    message += this + "\n";
                });
            }
        });
        alert(message);
    }
}




// https://stackoverflow.com/a/16025232
$.fn.clearValidation = function () {
    var v = $(this).validate();
    $("[name]", this).each(function () {
        v.successList.push(this);
        v.showErrors();
    });
    v.resetForm();
    v.reset();
};

$.fn.clearFormElements = function () {
    var f = $(this);
    f.find(":input").each(function () {
        switch (this.type) {
        case "password":
        case "text":
        case "textarea":
        case "file":
        case "select-one":
            $(this).val("").trigger("change");
            break;
        case "select-multiple":
            $(this).val("").trigger("change");
            break;
        case "date":
        case "number":
        case "tel":
        case "email":
            $(this).val("");
            break;
        case "checkbox":
            this.checked = false;
            $(this).removeAttr("checked").iCheck("update");
            var classes = $(this).parent().closest("div");
            $(classes).removeClass("checked ");
            break;
        case "radio":
            this.checked = false;
            $(this).iCheck("uncheck").iCheck("update");
            break;
        }
    });
}