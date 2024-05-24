import * as signalR from '@microsoft/signalr'
import React, { useEffect, useState } from 'react'

const CurrencyTracker = () => {
	const [currencyRates, setCurrencyRates] = useState([])

	useEffect(() => {
		const connection = new signalR.HubConnectionBuilder()
			.withUrl('https://localhost:7228/currency-rates')
			.configureLogging(signalR.LogLevel.Information)
			.build()

		connection.on('ReceiveCurrencyRate', (rate) => {
			setCurrencyRates((prevRates) => {
				const existingRateIndex = prevRates.findIndex((r) => r.currency === rate.currency)
				if (existingRateIndex >= 0) {
					const updatedRates = [...prevRates]
					updatedRates[existingRateIndex] = rate
					return updatedRates
				} else {
					return [...prevRates, rate]
				}
			})
		})

		connection
			.start()
			.then(() => console.log('Connection established'))
			.catch((err) => console.error('Connection failed: ', err))

		return () => {
			connection
				.stop()
				.then(() => console.log('Connection stopped'))
				.catch((err) => console.error('Error stopping connection: ', err))
		}
	}, [])

	return (
		<div className='container'>
			<h1>Currency Rates</h1>
			<ul>
				{currencyRates.map((rate, index) => (
					<li key={index}>
						{rate.currency}: {rate.rate}
					</li>
				))}
			</ul>
		</div>
	)
}

export default CurrencyTracker
