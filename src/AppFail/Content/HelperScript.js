var appfail = function () {
    function show(errorData) {
        var newDiv = document.createElement('div');
        newDiv.setAttribute('id', 'appFailReport');

        var newUl = document.createElement('ul');

        for (var i = 0, len = errorData.length; i < len; i++) {
            var error = errorData[i];
            var newLi = document.createElement('li');
            var newText = document.createTextNode('Error: "' + error.Name + '" occurred ' + error.Occurrences + ' times');
            newLi.appendChild(newText);
            newUl.appendChild(newLi);
        }

        newDiv.appendChild(newUl);

        var bodies = document.getElementsByTagName('body');
        if (!bodies.length) {
            return;
        }
        var body = bodies[0];
        body.appendChild(newDiv);
    }

    return {
        show: show
    }
} ();