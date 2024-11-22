# CloudQA Assessment

This project is a .NET application designed to automate form testing using Selenium WebDriver. The application performs various field tests on a sample form hosted at [https://app.cloudqa.io/home/AutomationPracticeForm](https://app.cloudqa.io/home/AutomationPracticeForm).

## Project Structure

- **Prerequisites**
	- .NET 6.0 SDK
	- Docker
	- Docker Compose

- **Setup**
	1. Clone the repository:
		 ```sh
		 git clone <repository-url>
		 ```
	2. Build the Docker image:
		 ```sh
		 docker-compose up --build
		 ```

- **Running the Tests**
	The tests are executed automatically when the Docker container is started. The results and screenshots are saved in the `screenshots` directory.

## Project Files

- **Program.cs**: Contains the main logic for initializing the WebDriver, performing field tests, and capturing screenshots.
- **CloudQAAssessment.csproj**: Project file containing dependencies.
- **Dockerfile**: Dockerfile for building the application image.
- **compose.yml**: Docker Compose file for setting up the container environment.
- **build.log**: Log file for the build process.
- **screenshots/**: Directory where screenshots are saved.

## Key Classes and Methods

- **RobustFormTest**: Main class containing methods to test various form fields.
	- **InitializeDriver()**: Initializes the Chrome WebDriver with necessary options.
	- **FindElementWithMultipleStrategies(By[] locators)**: Finds an element using multiple locator strategies.
	- **TestFirstNameField()**: Tests the First Name field.
	- **TestLastNameField()**: Tests the Last Name field.
	- **TestEmailField()**: Tests the Email field.
	- **TestMobileNumberField()**: Tests the Mobile Number field.
	- **TestGenderField()**: Tests the Gender field.
	- **TestDateOfBirthField()**: Tests the Date of Birth field.
	- **TakeScreenshot(string scenarioName)**: Captures a screenshot and saves it to the `screenshots` directory.
	- **RunAutomationTests()**: Runs all the field tests and captures screenshots.

## Example Usage

To run the tests, simply start the Docker container:
```sh
docker-compose up
```

## Contributing

Contributions are welcome! Please fork the repository and create a pull request with your changes.

## License

This project is licensed under the GNU Affero General Public License (AGPL-3.0). See the [LICENSE](https://github.com/khadeshyam/CloudQA-Assessment/blob/master/LICENSE) file for details.
