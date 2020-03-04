"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.confirmPasswordMatch = function (control) {
    var password = control.get('password');
    var confirmPassword = control.get('confirmPassword');
    return password && confirmPassword && password.value !== confirmPassword.value ? { 'confirmPasswordMismatch': true } : null;
};
//# sourceMappingURL=confirmpassword.directive.js.map