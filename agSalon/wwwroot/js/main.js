
const menuArrows = document.querySelectorAll('.menu__link-name');

if (menuArrows.length > 0) {
    for (let arrow of menuArrows) {
        arrow.addEventListener('click', function () {
            let links = document.querySelectorAll('.menu__link');
            let arrowParent = arrow.parentElement;
            for (let link of links) {
                if (link != arrowParent)
                    link.classList.remove('active');
            }
            arrowParent.classList.toggle('active');
        });
    }
}


const deletes_a = document.querySelectorAll('.delete-confirm');
if (deletes_a.length > 0) {
    for (let a of deletes_a) {
        a.addEventListener('click', function () {
            let deletes = document.querySelectorAll('.delete');
            let parentA = a.parentElement;

            for (let del of deletes) {
                if (del != parentA)
                    del.classList.remove('active');
            }

            parentA.classList.toggle('active');
        });
    }
}