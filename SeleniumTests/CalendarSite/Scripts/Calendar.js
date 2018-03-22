//class Calendar
function Calendar() {
    this.id = "";
    this.ParentElementId = "";
    this.CurrentMonth = 0;
    this.CurrentYear = 0;
    this.SelectedDate = new Date();
}

//static members
Calendar.count = 0; // начальное значение
Calendar.calendars = [];

Calendar.CreateCalendar = function (parentElementId) {
    var c = Calendar.GetCalendarObject(parentElementId);
    if (c != null)
        return;

    var calendar = new Calendar();
    calendar.ParentElementId = parentElementId;
    calendar.id = "Calendar" + Calendar.count;
    Calendar.count++;
    
    //var mon = month - 1; // месяцы в JS идут от 0 до 11, а не от 1 до 12
    var dc = new Date();
    calendar.CurrentMonth = dc.getMonth();
    calendar.CurrentYear = dc.getFullYear();
    calendar.DrawCalendar();
    
    Calendar.calendars.push(calendar);
};

Calendar.DeleteCalendar = function (parentElementId) {
    for (var i = 0; i < Calendar.calendars.length; i++) {
        var c = Calendar.calendars[i];
        if (c == null)
            continue;
        if (c.ParentElementId == parentElementId) {
            c.ClearCalendar();
            Calendar.calendars.splice(i, 1);
            break;
        }
    }
};

Calendar.GetSelectedDate = function(parentElementId) {
    var c = Calendar.GetCalendarObject(parentElementId);
    if (c != null)
        return c.GetSelectedDate();
    return null;
};

Calendar.SetSelectedDate = function (parentElementId, year, month, date) {
    if (isNaN(year) || isNaN(month) || isNaN(date))
        return;
    var c = Calendar.GetCalendarObject(parentElementId);
    if (c != null)
        c.SetFullSelectedDate(year, month, date);
};

Calendar.btnCalendarClick = function (event) {
    event = event || window.event;
    var btnId = event.target.id;
    var pos = btnId.indexOf("btn");
    var calendarId = btnId.substring(0, pos);
    var isMonth = btnId.indexOf("Month") > -1;
    var isForward = btnId.indexOf("Forward") > -1;

    for (var i = 0; i < Calendar.calendars.length; i++) {
        var c = Calendar.calendars[i];
        if (c == null)
            continue;
        if (c.id == calendarId) {
            if (isMonth && isForward) {
                c.GoForwardMonth();
            } else if (isMonth && !isForward) {
                c.GoBackMonth();
            } else if (!isMonth && isForward) {
                c.GoForwardYear();
            } else if (!isMonth && !isForward) {
                c.GoBackYear();
            }
            break;
        }
    }
};

Calendar.cellClick = function (event) {
    event = event || window.event;
    var date = event.target.innerText;
    if (date === undefined)
        date = event.target.textContent;
    event.target.textContent;
    var obj = event.target;
    while (obj.tagName.toUpperCase() != "TABLE") {
        obj = obj.parentElement;
    }
    var parentElementId = obj.parentElement.id;

    var c = Calendar.GetCalendarObject(parentElementId);
    if (c != null)
        c.SetSelectedDate(date);
};

Calendar.GetCalendarObject = function(parentElementId) {
    for (var i = 0; i < Calendar.calendars.length; i++) {
        var c = Calendar.calendars[i];
        if (c == null)
            continue;
        if (c.ParentElementId == parentElementId) {
            return c;
        }
    }
    return null;
};

//instance members
Calendar.prototype.GoBackYear = function() {
    this.CurrentYear--;
    this.DrawCalendar();
};

Calendar.prototype.GoForwardYear = function () {
    this.CurrentYear++;
    this.DrawCalendar();
};

Calendar.prototype.GoBackMonth = function () {
    this.CurrentMonth--;
    this.DrawCalendar();
};

Calendar.prototype.GoForwardMonth = function () {
    this.CurrentMonth++;
    this.DrawCalendar();
};

Calendar.prototype.SetFullSelectedDate = function (year, month, date) {
    if (year >= 0)
        this.CurrentYear = year;
    if (month >=1 && month <=12)
        this.CurrentMonth = month - 1;
    this.SetSelectedDate(date);
};

Calendar.prototype.SetSelectedDate = function (date) {
    this.SelectedDate = new Date(this.CurrentYear, this.CurrentMonth, date);
    this.DrawCalendar();
};

Calendar.prototype.GetSelectedDate = function (date) {
    return formatDate(this.SelectedDate);
};

Calendar.prototype.DrawCalendar = function () {
    var elem = document.getElementById(this.ParentElementId);

    //var mon = month - 1; // месяцы в JS идут от 0 до 11, а не от 1 до 12
    var dc = new Date();
    var date = dc.getDate();
    var month = dc.getMonth();
    var year = dc.getFullYear();
    
    var d = new Date(this.CurrentYear, this.CurrentMonth);
    var table = '<table>' +
        '<tr><th colspan="7">' +
        '<input id="' + this.id + 'btnYearBack" type="button" value="<<" onclick="Calendar.btnCalendarClick(event)"/>' +
        '<input id="' + this.id + 'btnMonthBack" type="button" value="<" onclick="Calendar.btnCalendarClick(event)"/> ' +
        num2month(this.CurrentMonth) + ", " + this.CurrentYear +
        ' <input id="' + this.id + 'btnMonthForward" type="button" value=">" onclick="Calendar.btnCalendarClick(event)"/>' +
        '<input id="' + this.id + 'btnYearForward" type="button" value=">>" onclick="Calendar.btnCalendarClick(event)"/>' +
        '</th></tr>' +
        //добавить дни недели
        '<tr><th>пн</th><th>вт</th><th>ср</th><th>чт</th><th>пт</th><th>сб</th><th>вс</th></tr><tr>';

    // заполнить первый ряд от понедельника
    // и до дня, с которого начинается месяц
    // * * * | 1  2  3  4
    var i;
    for (i = 0; i < getDay(d) ; i++) {
        table += '<td></td>';
    }

    // ячейки календаря с датами
    while (d.getMonth() == this.CurrentMonth) {
        if (d.getDate() == date && d.getMonth() == month && d.getFullYear() == year)
            table += '<td class="currDayCell' +
                (d.getDate() == this.SelectedDate.getDate() && d.getMonth() == this.SelectedDate.getMonth() && d.getFullYear() == this.SelectedDate.getFullYear()
                    ? ' selectedDateCell' : '') +
                '" onclick="Calendar.cellClick(event)">' + d.getDate() + '</td>';
        else
            table += '<td' +
                (d.getDate() == this.SelectedDate.getDate() && d.getMonth() == this.SelectedDate.getMonth() && d.getFullYear() == this.SelectedDate.getFullYear()
                    ? ' class="selectedDateCell"' : '') +
                ' onclick="Calendar.cellClick(event)">' + d.getDate() + '</td>';

        if (getDay(d) % 7 == 6) { // вс, последний день - перевод строки
            table += '</tr><tr>';
        }

        d.setDate(d.getDate() + 1);
    }

    // добить таблицу пустыми ячейками, если нужно
    if (getDay(d) != 0) {
        for (i = getDay(d) ; i < 7; i++) {
            table += '<td></td>';
        }
    }

    // закрыть таблицу
    table += '</tr></table>';

    // только одно присваивание innerHTML
    elem.innerHTML = table;
};

Calendar.prototype.ClearCalendar = function() {
    var elem = document.getElementById(this.ParentElementId);
    elem.innerHTML = "";
};

//Utilities
function getDay(date) { // получить номер дня недели, от 0(пн) до 6(вс)
    var day = date.getDay();
    if (day == 0) day = 7;
    return day - 1;
}

function num2month(num) {
    var month = ['Январь', 'Февраль', 'Март', 'Апрель', 'Май', 'Июнь',
    'Июль', 'Август', 'Сентябрь', 'Октябрь', 'Ноябрь', 'Декабрь'];
    return month[num];
}

function formatDate(date) {
    var dd = date.getDate();
    if (dd < 10) dd = '0' + dd;

    var mm = date.getMonth() + 1;
    if (mm < 10) mm = '0' + mm;

    var yy = date.getFullYear();

    return yy + '/' + mm + '/' + dd;
}
