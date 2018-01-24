var express = require('express');
var app = require('express')();
var http = require('http').Server(app);
var path = require('path');

app.use(express.static(path.join(__dirname, 'Views/Movies')));

app.get('/', function (req, res) {
    res.send('<h1>Hello world</h1>');
});

http.listen(3300, function () {
    console.log('listening on *:3000');
});