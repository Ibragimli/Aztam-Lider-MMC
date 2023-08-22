document.addEventListener("DOMContentLoaded", function () {
    const galleryLinks = document.querySelectorAll(".documentListName");
    const images = document.querySelectorAll(".documentImage");

    galleryLinks.forEach(function (link, index) {
        link.addEventListener("click", function (event) {
            //event.preventDefault();
            images.forEach(function (image) {
                image.style.display = "none";
            });
            images[index].style.display = "block";
        });
    });
});
document.addEventListener("DOMContentLoaded", function () {
    const galleryLinks = document.querySelectorAll(".documentName");
    const images = document.querySelectorAll(".documentImage");

    galleryLinks.forEach(function (link, index) {
        link.addEventListener("click", function (event) {
            //event.preventDefault();
            images.forEach(function (image) {
                image.style.display = "none";
            });
            images[index].style.display = "block";
        });
    });
});
let slideIndex = 0;

function changeSlide(n) {
    showSlides(slideIndex += n);
}

function showSlides(n) {
    const slides = document.getElementsByClassName("slides")[0].children;
    if (n >= slides.length) {
        slideIndex = 0;
    } else if (n < 0) {
        slideIndex = slides.length - 1;
    }
    for (let i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    slides[slideIndex].style.display = "block";
}

showSlides(slideIndex);