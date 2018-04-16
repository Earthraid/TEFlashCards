
$(document).ready(function () {

    $('.add_tag_action, .remove_tag_action').click(function () {
        $(this).submit();
    });

    $('.edit_fields_action').on('focusout', function () {
        $(this).submit();
    });

});