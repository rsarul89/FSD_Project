import { AppPage } from './app.po';
import { browser, element, by } from 'protractor';
import { RouterTestingModule } from "@angular/router/testing";

describe('workout-tracker App', () => {
    let page: AppPage;

    beforeEach(() => {
        page = new AppPage();
        let LogoutButton = element(by.css(".logout-link"));
        LogoutButton.click();
    });


    it('Should be display login page', () => {
        page.navigateTo();
        expect(page.getLoginPageHtml()).toBeDefined();
    });


    it("Should be display register page", () => {

        browser.get("/")

        let RegisterButton = element(by.css(".btn-register"));
        RegisterButton.click();

        let register = element(by.css(".register-panel")).getInnerHtml();
        expect(register).not.toEqual('');
    });

    it("Should be display report page", () => {

        browser.get("/")
        let userNameInput = element(by.css("input[name=username]"));
        userNameInput.sendKeys("DemoUser");

        let passwordInput = element(by.css("input[name=password]"));
        passwordInput.sendKeys("DemoPassword");

        let LoginButton = element(by.css(".btn-login"));
        LoginButton.click();

        let header = element(by.css('.header')).getText();
        let workouts = element.all(by.css(".workout-list-container workout-list"));
        expect(header).toEqual('Workout Tracker');
        expect(workouts.count()).toBeGreaterThanOrEqual(0);

        let TrackButton = element(by.css(".links li:nth-child(4) a"));
        TrackButton.click();

    });

    it("Should be able to login", () => {

        browser.get("/")
        let userNameInput = element(by.css("input[name=username]"));
        userNameInput.sendKeys("DemoUser");

        let passwordInput = element(by.css("input[name=password]"));
        passwordInput.sendKeys("DemoPassword");

        let LoginButton = element(by.css(".btn-login"));
        LoginButton.click();

        let header = element(by.css('.header')).getText();
        let workouts = element.all(by.css(".workout-list-container workout-list"));
        expect(header).toEqual('Workout Tracker');
        expect(workouts.count()).toBeGreaterThanOrEqual(0);

    });

    it("Should be be able to register", () => {

        browser.get("/")

        let RegisterButtonLoginForm = element(by.css(".btn-register"));
        RegisterButtonLoginForm.click();

        let register = element(by.css(".register-panel")).getInnerHtml();

        let userNameInput = element(by.css("nput[name=username]"));
        userNameInput.sendKeys("DemoUserRegister");

        let passwordInput = element(by.css("input[name=password]"));
        passwordInput.sendKeys("DemoPasswordRegister");

        let RegisterButtonRegForm = element(by.css(".btn-register"));
        RegisterButtonRegForm.click();

        let successMessage = element(by.css(".alert-success")).getText();

        expect(register).toBeDefined();
        expect(successMessage).toEqual('Hi DemoUserRegister you are successfully registered');

    });

    it("Should be able to logout", () => {

        browser.get("/")
        let userNameInput = element(by.css("input[name=username]"));
        userNameInput.sendKeys("DemoUser");

        let passwordInput = element(by.css("input[name=password]"));
        passwordInput.sendKeys("DemoPassword");

        let LoginButton = element(by.css(".btn-login"));
        LoginButton.click();

        let LogoutButton = element(by.css(".logout-link"));
        LogoutButton.click();

        expect(page.getLoginPageHtml()).toBeDefined();

    });

    it("Should be show all workouts when we first load the workout tracker app", () => {

        browser.get("/")
        let userNameInput = element(by.css("input[name=username]"));
        userNameInput.sendKeys("DemoUser");

        let passwordInput = element(by.css("input[name=password]"));
        passwordInput.sendKeys("DemoPassword");

        let LoginButton = element(by.css(".btn-login"));
        LoginButton.click();

        let workouts = element.all(by.css(".workout-list-container workout-list"));
        expect(workouts.count()).toBeGreaterThanOrEqual(0);

    });

    it("Should be view all workouts when click View All link", () => {

        browser.get("/")
        let userNameInput = element(by.css("input[name=username]"));
        userNameInput.sendKeys("DemoUser");

        let passwordInput = element(by.css("input[name=password]"));
        passwordInput.sendKeys("DemoPassword");

        let LoginButton = element(by.css(".btn-login"));
        LoginButton.click();

        let header = element(by.css('.header')).getText();
        let workouts = element.all(by.css(".workout-list-container workout-list"));
        expect(header).toEqual('Workout Tracker');
        expect(workouts.count()).toBeGreaterThanOrEqual(0);

    });

    it("Should be able to create workout when click Create link", () => {

        browser.get("/")
        let userNameInput = element(by.css("input[name=username]"));
        userNameInput.sendKeys("DemoUser");

        let passwordInput = element(by.css("input[name=password]"));
        passwordInput.sendKeys("DemoPassword");

        let LoginButton = element(by.css(".btn-login"));
        LoginButton.click();

        let CreateButton = element(by.css(".links li:nth-child(2) a"));
        CreateButton.click();

        let header = element.all(by.css("h3#addWorkoutHeader")).getText();
        expect(header).toEqual('Add Workout');

        let AddWorkoutButton= element(by.css(".add-workout"));
        expect(AddWorkoutButton.isEnabled()).toEqual(false);

        let title = element(by.css("input[name=title]"));
        title.sendKeys("TestWorkoutTitle");

        let note = element(by.css("input[name=note]"));
        note.sendKeys("TestWorkoutNote");

        let calories = element(by.css("input[name=calories]"));

        expect(calories.getText()).toEqual(0);

        let IncrButton = element(by.css(".incr-btn"));
        IncrButton.click();

        expect(calories.getText()).toBeGreaterThanOrEqual(0.1);

        let DecrButton = element(by.css(".dcr-btn"));
        DecrButton.click();

        expect(calories.getText()).toBeGreaterThanOrEqual(0);

        let category = element(by.css("#inputCategory option:selected"));
        expect(category.getText()).toEqual('Please select a category');

        element(by.css("#inputCategory option:nth-child(2)")).click();

        expect(AddWorkoutButton.isEnabled()).toEqual(true);

        AddWorkoutButton.click();
  
    });

    it("should be able to search workout", () => {

        browser.get("/")
        let userNameInput = element(by.css("input[name=username]"));
        userNameInput.sendKeys("DemoUser");

        let passwordInput = element(by.css("input[name=password]"));
        passwordInput.sendKeys("DemoPassword");

        let LoginButton = element(by.css(".btn-login"));
        LoginButton.click();

        let header = element(by.css('.header')).getText();
        let workouts = element.all(by.css(".workout-list-container workout-list"));
        expect(header).toEqual('Workout Tracker');
        expect(workouts.count()).toBeGreaterThanOrEqual(0);

        let searchBox = element(by.css(".searchBox"));
        searchBox.sendKeys("TestWorkoutTitle");

        expect(workouts.count()).toBeGreaterThanOrEqual(0);

    });

    it("should be able to edit workout", () => {

        browser.get("/")
        let userNameInput = element(by.css("input[name=username]"));
        userNameInput.sendKeys("DemoUser");

        let passwordInput = element(by.css("input[name=password]"));
        passwordInput.sendKeys("DemoPassword");

        let LoginButton = element(by.css(".btn-login"));
        LoginButton.click();

        let header = element(by.css('.header')).getText();
        let workouts = element.all(by.css(".workout-list-container workout-list"));
        expect(header).toEqual('Workout Tracker');
        expect(workouts.count()).toBeGreaterThanOrEqual(0);

        let linkEdit = element(by.css(".edit-workout"));
        linkEdit.click();

        let header1 = element(by.css('#editWorkoutHeader')).getText();
        expect(header).toEqual('Edit Workout');

        let title = element(by.css('input[name = title]'));
        let note = element(by.css('input[name = note]'));

        let category = element(by.css("#inputCategory option:selected"));
        expect(category.getText()).not.toEqual('Please select a category');

        let btnEdit = element(by.css(".btn-update"));
        btnEdit.click();

    });

     it("should be able to edit workout", () => {

        browser.get("/")
        let userNameInput = element(by.css("input[name=username]"));
        userNameInput.sendKeys("DemoUser");

        let passwordInput = element(by.css("input[name=password]"));
        passwordInput.sendKeys("DemoPassword");

        let LoginButton = element(by.css(".btn-login"));
        LoginButton.click();

        let header = element(by.css('.header')).getText();
        let workouts = element.all(by.css(".workout-list-container workout-list"));
        expect(header).toEqual('Workout Tracker');
        expect(workouts.count()).toBeGreaterThanOrEqual(0);

        let linkEdit = element(by.css(".edit-workout"));
        linkEdit.click();

        let header1 = element(by.css('#editWorkoutHeader')).getText();
        expect(header).toEqual('Edit Workout');

        let title = element(by.css('input[name = title]'));
        let note = element(by.css('input[name = note]'));

        let category = element(by.css("#inputCategory option:selected"));
        expect(category.getText()).not.toEqual('Please select a category');

        let btnEdit = element(by.css(".btn-update"));
        btnEdit.click();

    });

    it("should be able to delete workout", () => {

        browser.get("/")
        let userNameInput = element(by.css("input[name=username]"));
        userNameInput.sendKeys("DemoUser");

        let passwordInput = element(by.css("input[name=password]"));
        passwordInput.sendKeys("DemoPassword");

        let LoginButton = element(by.css(".btn-login"));
        LoginButton.click();

        let header = element(by.css('.header')).getText();
        let workouts = element.all(by.css(".workout-list-container workout-list"));
        expect(header).toEqual('Workout Tracker');
        expect(workouts.count()).toBeGreaterThanOrEqual(0);
        let oldCount = workouts.count();

        let btnDelete = element(by.css(".delete-workout:first"));
        btnDelete.click();

        expect(workouts.count()).not.toEqual(oldCount);

        let successMessage = element(by.css('.alert-success'));
        expect(successMessage.getText()).toEqual('Workout deleted successfully');

    });

    it("should be able to start workout", () => {

        browser.get("/")
        let userNameInput = element(by.css("input[name=username]"));
        userNameInput.sendKeys("DemoUser");

        let passwordInput = element(by.css("input[name=password]"));
        passwordInput.sendKeys("DemoPassword");

        let LoginButton = element(by.css(".btn-login"));
        LoginButton.click();

        let header = element(by.css('.header')).getText();
        let workouts = element.all(by.css(".workout-list-container workout-list"));
        expect(header).toEqual('Workout Tracker');
        expect(workouts.count()).toBeGreaterThanOrEqual(0);

        let linkEdit = element(by.css(".edit-workout"));
        linkEdit.click();

        let header1 = element(by.css('#workoutStartHeader')).getText();
        expect(header).toEqual('Start Workout');

        let title = element(by.css('input[name = title]'));
        let note = element(by.css('input[name = note]'));
        let sd = element(by.css('#inputSd'));
        let st = element(by.css('#inputSt'));

        expect(title).not.toEqual('');
        expect(note).not.toEqual('');
        expect(sd).not.toEqual('');
        expect(st).not.toEqual('');

        let btnStart = element(by.css(".btn-start"));
        btnStart.click();

        let successMessage = element(by.css('.alert-success'));
        expect(successMessage.getText()).toEqual('Workout started successfully');

    });

    it("should be able to end workout", () => {

        browser.get("/")
        let userNameInput = element(by.css("input[name=username]"));
        userNameInput.sendKeys("DemoUser");

        let passwordInput = element(by.css("input[name=password]"));
        passwordInput.sendKeys("DemoPassword");

        let LoginButton = element(by.css(".btn-login"));
        LoginButton.click();

        let header = element(by.css('.header')).getText();
        let workouts = element.all(by.css(".workout-list-container workout-list"));
        expect(header).toEqual('Workout Tracker');
        expect(workouts.count()).toBeGreaterThanOrEqual(0);

        let linkEdit = element(by.css(".edit-workout"));
        linkEdit.click();

        let header1 = element(by.css('#endWorkoutHeader')).getText();
        expect(header).toEqual('End Workout');

        let title = element(by.css('#inputTitle'));
        let comment = element(by.css('#inputComment'));
        let ed = element(by.css('#inputEd'));
        let et = element(by.css('#inputEt'));

        expect(title).not.toEqual('');
        expect(comment).not.toEqual('');
        expect(ed).not.toEqual('');
        expect(et).not.toEqual('');

        let btnEnd = element(by.css(".btn-end"));
        btnEnd.click();

        let successMessage = element(by.css('.alert-success'));
        expect(successMessage.getText()).toEqual('Workout ended successfully');

    });

    it("should be able to create workout category when click Category link", () => {

        browser.get("/")
        let userNameInput = element(by.css("input[name=username]"));
        userNameInput.sendKeys("DemoUser");

        let passwordInput = element(by.css("input[name=password]"));
        passwordInput.sendKeys("DemoPassword");

        let LoginButton = element(by.css(".btn-login"));
        LoginButton.click();

        let CategoryButton = element(by.css(".links li:nth-child(3) a"));
        CategoryButton.click();

        let header = element.all(by.css("h3#categoryCreateHeader")).getText();
        expect(header).toEqual('Add Category');

        let categories = element.all(by.css(".category-list-container category-list"));
        expect(categories.count()).toBeGreaterThanOrEqual(0);

        let AddWorkoutCategoryButton = element(by.css(".btn-add"));
        expect(AddWorkoutCategoryButton.isEnabled()).toEqual(false);

        let requiredMessage = element(by.css(".error"));
        expect(requiredMessage.getText()).toEqual('Category title required.');

        let title = element(by.css(".addBox"));
        title.sendKeys("TestWorkoutCategoryTitle");

        expect(requiredMessage).toBeUndefined();
        expect(AddWorkoutCategoryButton.isEnabled()).toEqual(true);

        AddWorkoutCategoryButton.click();

    });

    it("should be able to search workout category", () => {

        browser.get("/")
        let userNameInput = element(by.css("input[name=username]"));
        userNameInput.sendKeys("DemoUser");

        let passwordInput = element(by.css("input[name=password]"));
        passwordInput.sendKeys("DemoPassword");

        let LoginButton = element(by.css(".btn-login"));
        LoginButton.click();

        let CategoryButton = element(by.css(".links li:nth-child(3) a"));
        CategoryButton.click();

        let header = element.all(by.css("h3#categoryCreateHeader")).getText();
        expect(header).toEqual('Add Category');

        let categories = element.all(by.css(".category-list-container category-list"));
        expect(categories.count()).toBeGreaterThanOrEqual(0);

        let searchBox = element(by.css(".searchBox"));
        searchBox.sendKeys("TestWorkoutCategoryTitle");

        expect(categories.count()).toBeGreaterThanOrEqual(0);

    });

    it("should be able to edit category", () => {

        browser.get("/")
        let userNameInput = element(by.css("input[name=username]"));
        userNameInput.sendKeys("DemoUser");

        let passwordInput = element(by.css("input[name=password]"));
        passwordInput.sendKeys("DemoPassword");

        let LoginButton = element(by.css(".btn-login"));
        LoginButton.click();

        let CategoryButton = element(by.css(".links li:nth-child(3) a"));
        CategoryButton.click();

        let header = element.all(by.css("h3#categoryCreateHeader")).getText();
        expect(header).toEqual('Add Category');

        let categories = element.all(by.css(".category-list-container category-list"));
        expect(categories.count()).toEqual(3);

        let EditCategoryButton = element(by.css(".edit-cat:first"));

        EditCategoryButton.click();

        let header1 = element.all(by.css("h3#editCatHeader")).getText();
        expect(header1).toEqual('Edit Category');

        let title = element(by.css('#inputTitle'));

        expect(title).not.toEqual('');

        let btnEnd = element(by.css(".btn-update"));
        btnEnd.click();

        let successMessage = element(by.css('.alert-success'));
        expect(successMessage.getText()).toEqual('Workout category updated successfully');

    });

    it("should be able to delete category", () => {

        browser.get("/")
        let userNameInput = element(by.css("input[name=username]"));
        userNameInput.sendKeys("DemoUser");

        let passwordInput = element(by.css("input[name=password]"));
        passwordInput.sendKeys("DemoPassword");

        let LoginButton = element(by.css(".btn-login"));
        LoginButton.click();

        let CategoryButton = element(by.css(".links li:nth-child(3) a"));
        CategoryButton.click();

        let header = element.all(by.css("h3#categoryCreateHeader")).getText();
        expect(header).toEqual('Add Category');

        let categories = element.all(by.css(".category-list-container category-list"));
        expect(categories.count()).toBeGreaterThanOrEqual(0);

        let DeleteCategoryButton = element(by.css(".delete-cat:first"));

        DeleteCategoryButton.click();

        let oldCount = categories.count();

        let btnEdit = element(by.css(".delete-workout:first"));
        btnEdit.click();

        expect(categories.count()).not.toEqual(oldCount);

        let successMessage = element(by.css('.alert-success'));
        expect(successMessage.getText()).toEqual('Category deleted successfully');

    });
});
