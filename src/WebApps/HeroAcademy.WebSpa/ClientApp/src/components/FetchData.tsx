import React, { Component } from 'react';

interface Forecast {
    dateFormatted: string, // JSON dates are just strings.
    temperatureC: number,
    temperatureF: number,
    summary: string
};

export interface FetchDataProps {
};

interface FetchDataState {
    forecasts: Forecast[],
    loading: boolean
};

export class FetchData extends Component<FetchDataProps, FetchDataState> {
    static displayName = FetchData.name;

    constructor(props: FetchDataProps) {
        super(props);
        this.state = { forecasts: [], loading: true };

        //TODO: URL should be in settings.
        fetch('https://localhost:8001/api/v1/SampleData/WeatherForecasts')
            .then(response => response.json())
            .then(data => {
                this.setState({ forecasts: data, loading: false });
            });
    }

    static renderForecastsTable(forecasts: Forecast[]) {
        return (
            <table className='table table-striped'>
                <thead>
                    <tr>
                        <th>Date</th>
                        <th>Temp. (C)</th>
                        <th>Temp. (F)</th>
                        <th>Summary</th>
                    </tr>
                </thead>
                <tbody>
                    {forecasts.map((forecast: Forecast) =>
                        <tr key={forecast.dateFormatted}>
                            <td>{forecast.dateFormatted}</td>
                            <td>{forecast.temperatureC}</td>
                            <td>{forecast.temperatureF}</td>
                            <td>{forecast.summary}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : FetchData.renderForecastsTable(this.state.forecasts);

        return (
            <div>
                <h1>Weather forecast</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
        );
    }
}
