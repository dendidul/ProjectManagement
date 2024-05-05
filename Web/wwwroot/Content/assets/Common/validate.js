var Validate = function () {
    var i = function () {
        $(".persada-validate").validate({
            errorElement: "span",
            errorClass: "help-block",
            focusInvalid: !1,
            ignore: "",
            //rules: {
            //    required: {
            //        required: !0
            //    }
            //},
            highlight: function (i) {
                $(i).closest(".form-group").addClass("has-error")
            },
            success: function (i) {
                i.closest(".form-group").removeClass("has-error"), i.remove()
            },
            errorPlacement: function (i, r) {
                r.closest(".input-icon").size() ? i.insertAfter(r.closest(".input-icon")) : i.insertAfter(r);
            },
            submitHandler: function (i) {
                i.submit()
            }
        }), $(".persada-validate input").keypress(function (e) {
            if (13 == i.which) return $(".persada-validate").validate().form() && $(".persada-validate").submit(), !1
        }), $(".persada-validate input").change(function (e) {
            if (13 == i.which) return $(".persada-validate").validate().form() && $(".persada-validate").submit(), !1
        })
    };
    return {
        init: function () {
            i()
        }
    }
}();
jQuery(document).ready(function () {
    Validate.init()
});