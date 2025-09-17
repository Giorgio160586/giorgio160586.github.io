/**
* Updated: 01/11/2024
*/

(function() {
    document.addEventListener('DOMContentLoaded', function () {
    // Rileva la lingua dalla URL o ottieni la lingua del browser
    const urlParams = new URLSearchParams(window.location.search);
    const selectedLang = urlParams.get('lang');
		
    // Se è stata selezionata una lingua, usala; altrimenti, usa la lingua del browser
    const userLang = selectedLang || (navigator.language || navigator.userLanguage);
    const lang = userLang.startsWith('it') ? 'it' : 'en'; // Imposta 'it' se la lingua inizia con 'it', altrimenti 'en'
		
    // Nascondi tutti gli elementi con la lingua non corrispondente
    document.querySelectorAll('[data-lang]').forEach(el => {
      if (el.getAttribute('data-lang') !== lang) {
          el.style.display = 'none';
        } else {
          el.style.display = 'inline'; // Mostra l'elemento corretto
        }
        });
      });

})();