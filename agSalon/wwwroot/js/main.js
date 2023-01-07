console.log(1);

const menuArrows = document.querySelectorAll('.menu__arrow');

if (menuArrows.length > 0) {
    for (let arrow of menuArrows) {
        arrow.addEventListener('click', function () {
            let links = document.querySelectorAll('.menu__link');
            for (let link of links) {
                if (link != arrow.parentElement)
                    link.classList.remove('active');
            }
            arrow.parentElement.classList.toggle('active');
        });
    }
}