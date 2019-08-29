import React, { Component } from 'react';

export class Courses extends Component {
    static displayName = Courses.name;

    constructor(props) {
        super(props);
        this.state = { courses: [], loading: true };

        //TODO: URL should be in settings.
        fetch('http://localhost:19639/api/v1/courses')
            .then(response => response.json())
            .then(data => {
                this.setState({ courses: data, loading: false });
            });
    }

    static renderCoursesTable(courses) {
        return (
            <table className='table table-striped'>
                <thead>
                    <tr>
                        <th>Code</th>
                        <th>Title</th>
                        <th>Description</th>
                        <th>Instructor</th>
                    </tr>
                </thead>
                <tbody>
                    {courses.map(course =>
                        <tr key={course.courseId}>
                            <td>{course.referenceCode}</td>
                            <td>{course.titleEn}</td>
                            <td>{course.descriptionEn}</td>
                            <td>{course.instructor.name}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Courses.renderCoursesTable(this.state.courses);

        return (
            <div>
                <h1>Courses</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
        );
    }
}
