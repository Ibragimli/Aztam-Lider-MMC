document.addEventListener("DOMContentLoaded", function () {
    var count = 0;
    var refImageBoxes = document.querySelectorAll(".refInfoImageBox");
    var prevBtn = document.querySelector(".prev-reference-btn");
    var nextBtn = document.querySelector(".next-reference-btn");

    function showSlide(index) {
        for (var i = 0; i < refImageBoxes.length; i++) {
            refImageBoxes[i].style.display = "none";
        }
        refImageBoxes[index].style.display = "block";

        if (refImageBoxes.length <= 1) {
            prevBtn.style.display = "none";
            nextBtn.style.display = "none";
        } else {
            prevBtn.style.display = "block";
            nextBtn.style.display = "block";
        }
    }

    function nextSlide() {
        count = (count + 1) % refImageBoxes.length;
        showSlide(count);
    }

    function prevSlide() {
        count = (count - 1 + refImageBoxes.length) % refImageBoxes.length;
        showSlide(count);
    }

    prevBtn.addEventListener("click", prevSlide);
    nextBtn.addEventListener("click", nextSlide);

    // İlk slaydı göster
    showSlide(count);
});
