import axios from 'axios'
import { GetWebScraperResultsResponse } from '../models/getWebScraperResultsResponse'

const baseUrl = 'https://localhost:7263/WebScraper'

export const GetWebScraperResults = async (url: string, keyword: string): Promise<GetWebScraperResultsResponse> => {
    try {
        const res = await axios.get(`${baseUrl}?url=${url}&keyword=${keyword}`)
        if (res.status === 200) {
            return {occurrenceInfoDto: { occurrences: res.data.info.occurrences, occurrenceString: res.data.info.occurrenceString}}
        }
        return {occurrenceInfoDto: { occurrences: 0, occurrenceString: ""}}
    } catch (err) {
        console.log("this is error", err)
        return {occurrenceInfoDto: { occurrences: 0, occurrenceString: ""}, error: true}
    }
} 