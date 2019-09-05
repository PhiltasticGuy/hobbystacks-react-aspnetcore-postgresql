import React, { Component } from 'react';

interface Course {
    courseId: number,
    referenceCode: string,
    titleEn: string,
    descriptionEn: string,
    instructor: Instructor
};

interface Instructor {
    instructorId: number,
    name: string,
    titleEn: string,
    email: string,
    phoneNumber: string
};

export interface CoursesProps {
};

interface CoursesState {
    courses: Course[],
    loading: boolean
};

export class Courses extends Component<CoursesProps, CoursesState> {
    static displayName = Courses.name;

    constructor(props: CoursesProps) {
        super(props);
        this.state = { courses: [], loading: true };

        //TODO: URL should be in settings.
        fetch('http://localhost:8000/api/v1/courses')
            .then(response => response.json())
            .then(data => {
                this.setState({ courses: data, loading: false });
            });
    }

    static renderCoursesTable(courses: Course[]) {
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
                    {courses.map((course: Course) =>
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
