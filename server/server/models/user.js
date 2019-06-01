const mongoose = require('mongoose');
const encryption = require('../utilities/encryption');

const ERROR_VALIDATION_MESSAGE = '${PATH} is required';

let userSchema = new mongoose.Schema(({
    username: { type: String, required: ERROR_VALIDATION_MESSAGE, unique: true},
    password: { type: String, required: ERROR_VALIDATION_MESSAGE },
    firstName: { type: String, required: ERROR_VALIDATION_MESSAGE },
    lastName: { type: String },
    email: { type: String, required: ERROR_VALIDATION_MESSAGE },
    roles: [{ type: String }],
    salt: { type: String, required: ERROR_VALIDATION_MESSAGE }
}));

userSchema.method({
    authenticate: function(password) {
        let hashedPassword = encryption.generateHashedPassword(this.salt, password);
        
        if (hashedPassword === this.password) {
            return true;
        }
        return false;
    }
});

const User = mongoose.model('User', userSchema);

module.exports = User