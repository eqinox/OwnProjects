const express = require('express');
let app = express();
let environment = process.env.NODE_ENV || 'development';
let settings = require('./server/config/settings')[environment];

let port = settings.port;

require('./server/config/database')(settings);
require('./server/config/express')(app, settings);

app.listen(port, () => {
    console.log(`Server is listening on port ${port}`);
});