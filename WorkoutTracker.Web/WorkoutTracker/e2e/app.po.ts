import { browser, by, element } from 'protractor';

export class AppPage {
  navigateTo() {
    return browser.get('/');
  }

    getLoginPageHtml() {
        return element(by.css('workout-tracker .login-panel')).getInnerHtml();
  }
}
