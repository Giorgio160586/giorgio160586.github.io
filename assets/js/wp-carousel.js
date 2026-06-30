(function () {
  function initCarousel(el) {
    const slides = el.querySelectorAll('.wpc-slide');
    const dotsContainer = el.querySelector('.wpc-dots');
    if (!slides.length) return;
    let current = 0;

    slides.forEach(function (_, i) {
      const dot = document.createElement('span');
      dot.className = 'wpc-dot' + (i === 0 ? ' active' : '');
      dot.addEventListener('click', function () { goto(i); });
      dotsContainer.appendChild(dot);
    });

    function goto(n) {
      slides[current].classList.remove('active');
      dotsContainer.querySelectorAll('.wpc-dot')[current].classList.remove('active');
      current = (n + slides.length) % slides.length;
      slides[current].classList.add('active');
      dotsContainer.querySelectorAll('.wpc-dot')[current].classList.add('active');
    }

    el.querySelector('.wpc-prev').addEventListener('click', function () { goto(current - 1); });
    el.querySelector('.wpc-next').addEventListener('click', function () { goto(current + 1); });
  }

  document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.wp-carousel').forEach(initCarousel);
  });
})();
