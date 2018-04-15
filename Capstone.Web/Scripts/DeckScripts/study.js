var nextMessage = 'Next card';
var flipped = false;


function flipCard() {
    $('.cardFront').addClass('hiddenSide');
    $('.cardBack').removeClass('hiddenSide');
    $('.studyNav').text(nextMessage);
}


function nextCard() {
    $('.cardBack').addClass('hiddenSide');
    $('.cardFront').removeClass('hiddenSide');

    var thisCard = $('.activeCard');
    var nextCard = thisCard.next();

    thisCard.removeClass('activeCard').addClass('oldCard');
    nextCard.removeClass('newCard').addClass('activeCard');

    $('.studyNav').text('Flip Card');
}


$(document).ready(function () {

    $('.resultsFrame').hide();
    var cardCount = parseInt($('#cardCount').data('name'));
    var totalCorrect = 0;
    $('.totalCorrect').text(totalCorrect);
    var cardsViewed = 0;
    $('.totalViewed').text(cardsViewed);


    $('.studyNav').text('Flip Card');
    $('.studyCard').first().removeClass('newCard').addClass('activeCard');


    $('.studyNav').click(function () {

        if (flipped == false) {

            flipCard();
            flipped = true;

        } else {
            cardsViewed++;
            $('.totalViewed').text(cardsViewed);

            if (cardsViewed == cardCount) {
                $('.studyFrame').hide();
                $('.resultsFrame').show();
            }
            else {
                if (cardsViewed == cardCount - 1) {
                    nextMessage = 'Finish';
                }
                nextCard();
                flipped = false;

            }
        }
    });

});