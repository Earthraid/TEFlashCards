/// <reference path="../jquery-3.1.1.js" />




$(document).ready(function () {

    $('.add_tag_action, .remove_tag_action').click(function () {
        $(this).submit();
    });

    $('.edit_fields_action').on('focusout', function () {
        $(this).submit();
    });



    function removeTagFromDeck(deckID, tagName) {

        var args = {};
        args.DeckID = deckID;
        args.TagName = tagName;

        $.ajax({
            type: "POST",
            url: "~/Deck/RemoveDeckTag",
            contentType: "application/json; charset=utf-8",
            data: args,
            dataType: "json",
            success: function (msg) {
                // Something afterwards here

            }
        });

    }
});
