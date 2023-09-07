function row(party) {
    const col = document.createElement("div");
    col.className = "col album-col";

    const card = document.createElement("div");
    card.className = "card shadow-sm";

    const svg = document.createElement("svg");
    svg.className = "bd-placeholder-img card-img-top";
    svg.style.width = "100%";
    svg.style.height = "225px";
    svg.style.background = "#55595c";

    const rect = document.createElement("rect");
    rect.style.width = "100%";
    rect.style.height = "100%";
    svg.append(rect);

    const text = document.createElement("text");
    text.style.position = "relative";
    text.style.top = "50%";
    text.style.left = "25%";
    text.style.color = "#eceeef";
    text.append(party.name);
    svg.append(text);

    card.append(svg);

    const card_body = document.createElement("div");
    card_body.className = "card-body";

    const items_row = document.createElement("div");
    items_row.className = "items-row";

    const p = document.createElement("p");
    p.className = "party-text";
    p.append(party.description);
    items_row.append(p);

    card_body.append(items_row);

    const d_flex = document.createElement("div");
    d_flex.className = "d-flex justify-content-between align-items-center";

    const btn_group = document.createElement("div");
    btn_group.className = "btn-group";

    const btn_add = document.createElement("button");
    btn_add.className = "btn btn-sm btn-outline-secondary";
    btn_add.innerText = "Add";
    btn_add.addEventListener("click", async () => await addStudent(party.id));
    btn_group.append(btn_add);

    const btn_show = document.createElement("button");
    btn_show.className = "btn btn-sm btn-outline-secondary";
    btn_show.innerText = "Show";
    btn_group.append(btn_show);

    const small = document.createElement("small");
    small.className = "text-body-secondary";
    small.append(party.students + " - students");
    small.setAttribute("id", party.id);
    d_flex.append(btn_group);
    d_flex.append(small);

    card_body.append(d_flex);

    card.append(card_body);

    col.append(card);

    return col;
}

function btnShowMore(switcher) {
    const show = document.getElementById("show-more");

    const position = document.createElement("div");
    position.className = "position-absolute top-50 start-50 translate-middle pb-3";

    const btn = document.createElement("button");
    btn.className = "border border-black p-3";
    btn.style.background = "none";
    btn.append("Show more");

    if (switcher) {
        btn.addEventListener("click", async () => await showMore());
    }
    else {
        btn.addEventListener("click", async () => await searchShowMore());
    }

    position.append(btn);
    show.append(position);
}

function deleteAll() {
    const parties = document.querySelector(".party-list");
    const show = document.getElementById("show-more");
    const error = document.getElementById("error");

    parties.innerHTML = "";
    show.innerHTML = "";
    error.innerHTML = "";
}

function deletePartyList() {
    const parties = document.querySelector(".party-list");
    parties.innerHTML = "";
}

function deleteShowMore() {
    const show = document.getElementById("show-more");
    show.innerHTML = "";
}

function deleteError() {
    const show = document.getElementById("show-more");
    error.innerHTML = "";
}

function notFound() {
    deleteAll();

    const parties = document.querySelector("#error");

    const position = document.createElement("div");
    position.className = "mb-3";
    position.style = "display: flex; justify-content: center;";

    const a = document.createElement("a");
    a.append("There are no similar parties!");

    position.append(a);
    parties.append(position);
}

function getCount(parties) {
    var count = 0;
    for (var key in parties) {
        count++;
    }

    return count;
}