const referenceSlide = document.querySelectorAll('.refInfoImageBox');
const referencePrevBtn = document.querySelector('.prev-reference-btn');
const referenceNextBtn = document.querySelector('.next-reference-btn');
let referenceCurrentSlide = 0;
let referencesSlideInterval;

//// İlk slaytı göster
//referenceSlide[referenceCurrentSlide].style.display = 'block';


// Slaytı bir sonraki slayta geçiren fonksiyon
function nextSlide() {
    referenceSlide[referenceCurrentSlide].style.display = 'none';
    referenceCurrentSlide = (referenceCurrentSlide + 1) % referenceSlide.length;
    referenceSlide[referenceCurrentSlide].style.display = 'block';
}

// Önceki slayta geç
referencePrevBtn.addEventListener('click', function () {
    referenceSlide[referenceCurrentSlide].style.display = 'none';
    referenceCurrentSlide = (referenceCurrentSlide - 1 + referenceSlide.length) % referenceSlide.length;
    referenceSlide[referenceCurrentSlide].style.display = 'block';
    clearInterval(referencesSlideInterval); // Ok tuşlarına tıklandığında otomatik dönme işlemini durdur
});

// Sonraki slayta geç
referenceNextBtn.addEventListener('click', function () {
    referenceSlide[referenceCurrentSlide].style.display = 'none';
    referenceCurrentSlide = (referenceCurrentSlide + 1) % referenceSlide.length;
    referenceSlide[referenceCurrentSlide].style.display = 'block';
    clearInterval(referencesSlideInterval); // Ok tuşlarına tıklandığında otomatik dönme işlemini durdur
});

// Resimler varsa ok tuşlarını gizle
if (referenceSlide.length <= 1) {
    referencePrevBtn.style.display = 'none';
    referenceNextBtn.style.display = 'none';
}

