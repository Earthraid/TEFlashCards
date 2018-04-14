/// <reference path="../jquery-3.1.1.js" />

$(document).ready(function () {
    //$(".removeTag").on("click", function () {
    //    var tagName = this.getAttribute("id");
    //    document.getElementById('TagToRemove').value = tagName;
    //    //some code to set the Model.TagName to the id of the button which is the tag
    //    var breakpoint = "pause";
    //});

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
})