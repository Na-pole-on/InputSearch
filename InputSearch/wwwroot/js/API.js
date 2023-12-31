﻿async function getUsers() {
    const response = await fetch("/api/parties", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });

    if (response.ok === true) {
        const parties = await response.json();
        const rows = document.querySelector(".party-list");

        deleteAll();

        parties.forEach(party => rows.append(row(party)));
        btnShowMore(0);
    }
    else {
        notFound();
    }
}

async function showMore() {
    const response = await fetch("/api/show", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });

    if (response.ok === true) {
        const parties = await response.json();
        const rows = document.querySelector(".party-list");

        if (getCount(parties) < 9) {
            deleteShowMore();
        }

        parties.forEach(party => rows.append(row(party)));
    }
}

async function search() {
    const search = document.getElementById("search").value;

    const response = await fetch("/api/search", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify(search)
    });

    if (response.ok === true) {
        const parties = await response.json();
        const rows = document.querySelector(".party-list");
        deleteAll();

        if (getCount(parties) < 9) {
            deleteShowMore();
        }
        else {
            btnShowMore(1);
        }

        parties.forEach(party => rows.append(row(party)));
    }
    else {
        notFound();
    }
}

async function searchShowMore() {
    const search = document.getElementById("search").value;

    const response = await fetch("/api/search", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify(search)
    });

    if (response.ok === true) {
        const parties = await response.json();
        const rows = document.querySelector(".party-list");

        if (getCount(parties) < 9) {
            deleteShowMore();
        }

        parties.forEach(party => rows.append(row(party)));
    }
}

async function addStudent(id) {
    const response = await fetch(`/api/parties/${id}`, {
        method: "GET",
        headers: { "Accept": "application/json" }
    });

    if (response.ok === true) {
        const students = document.getElementById(id);

        var str = students.innerHTML.indexOf(" ");
        var num = students.innerHTML.slice(0, str);

        students.innerHTML = (Number(num) + 1) + " - students";
    }

}

async function filter(id) {
    changeFilter(id);

    var alpha = document.getElementById("alphabetically")
        .getAttribute("value");
    var stu = document.getElementById("students")
        .getAttribute("value");
    var date = document.getElementById("date")
        .getAttribute("value");

    const response = await fetch("/api/filter", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            alphabetically: parseInt(alpha),
            students: parseInt(stu),
            date: parseInt(date)
        })
    });

    if (response.ok === true) {
        const parties = await response.json();
        const rows = document.querySelector(".party-list");
        deleteAll();

        if (getCount(parties) < 9) {
            deleteShowMore();
        }
        else {
            btnShowMore(-1);
        }

        parties.forEach(party => rows.append(row(party)));
    }
}

async function filterShowMore() {
    var alpha = document.getElementById("alphabetically")
        .getAttribute("value");
    var stu = document.getElementById("students")
        .getAttribute("value");
    var date = document.getElementById("date")
        .getAttribute("value");

    const response = await fetch("/api/filter", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            alphabetically: parseInt(alpha),
            students: parseInt(stu),
            date: parseInt(date)
        })
    });

    if (response.ok === true) {
        const parties = await response.json();
        const rows = document.querySelector(".party-list");

        if (getCount(parties) < 9) {
            deleteShowMore();
        }

        parties.forEach(party => rows.append(row(party)));
    }
}

getUsers();