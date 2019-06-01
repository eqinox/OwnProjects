const mongoose = require('mongoose');
const encryption = require('../utilities/encryption');
const logger = require('../utilities/logger');

// All Models must be loaded
require('../models/user');
const User = mongoose.model('User');

module.exports = (settings) => {
    mongoose.promise = global.Promise;
    mongoose.connect(settings.connectionString, { useNewUrlParser: true, useCreateIndex: true }).then().catch(err => {console.log(err)});

    let database = mongoose.connection;

    database.on('open', (err) => {
        if (err) {
            console.log(err);
            return
        }
        console.log('Database connected');        
    });

    seedAdminUser();

    database.on('error', (err) => {
        if (err) {
            logger.errorLog.error('Database Error:', { message: err })
        }
    });
}

// When database starts for the first time
function seedAdminUser() {
    User.find({})
        .then((users) => {
            if (users.length > 0) return;

                let salt = encryption.generateSalt();
                let hashedPassword = encryption.generateHashedPassword(salt, '1234');

                User.create({
                    username: 'Admin',
                    password: hashedPassword,
                    firstName: 'Admin',
                    lastName: 'Admin',
                    email: 'Admin@mail.bg',
                    roles: ['Admin'],
                    salt
                });
                logger.infoLog.info("Create User:", { message: 'Admin user created.' });
        });
}
