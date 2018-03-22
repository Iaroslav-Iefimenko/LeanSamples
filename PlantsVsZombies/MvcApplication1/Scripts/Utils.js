function extend(child, parent) {
    var f = function () {
    };
    f.prototype = parent.prototype;
    child.prototype = new f();
    child.prototype.constructor = child;
    child.superclass = parent.prototype;
}