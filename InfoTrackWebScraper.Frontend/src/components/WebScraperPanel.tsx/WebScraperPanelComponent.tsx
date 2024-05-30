import { ScraperResultText } from "./ScraperResultText"
import { Box, Button, TextField, Typography } from "@mui/material"
import { useState } from "react"
import { GetWebScraperResults } from "../../api/webScraperApi"

export const WebScraperPanelComponent = () => {
    const [urlValue, setUrlValue] = useState("")
    const [keywordValue, setKeywordValue] = useState("")
    const [resultVisible, setResultVisible] = useState(false)
    const [urlSearchValue, setUrlSearchValue] = useState("")
    const [keywordSearchValue, setKeywordSearchValue] = useState("")
    const [urlFieldTouched, setUrlFieldTouched] = useState(false)
    const [keywordFieldTouched, setkeywordFieldTouched] = useState(false)
    const [occurrenceLocations, setOccurrenceLocations] = useState("")
    const [matches, setMatches] = useState(0)
    const [loading, setLoading] = useState(false)
    const [scrapeError, setScrapeError] = useState(false)

    const onSearch = async () => {
        setLoading(true)
        setScrapeError(false)
        var encodedUrl = encodeURIComponent(urlValue)
        const { occurrenceInfoDto, error } = await GetWebScraperResults(
            encodedUrl,
            keywordValue
        )
        setScrapeError(error || false)
        setUrlSearchValue(urlValue.trim())
        setKeywordSearchValue(keywordValue.trim())
        setOccurrenceLocations(occurrenceInfoDto.occurrenceString)
        setMatches(occurrenceInfoDto.occurrences)
        setResultVisible(true)
        setLoading(false)
    }

    const onReset = () => {
        setUrlValue("")
        setKeywordValue("")
        setUrlFieldTouched(false)
        setkeywordFieldTouched(false)
        setResultVisible(false)
        setScrapeError(false)
    }

    return (
        <Box>
            <Box
                display={"flex"}
                flexDirection={"column"}
                alignItems={"center"}
            >
                <TextField
                    id="outlined-basic"
                    label="Url"
                    variant="outlined"
                    onFocus={() => setUrlFieldTouched(true)}
                    error={urlFieldTouched && urlValue === ""}
                    helperText={
                        urlFieldTouched &&
                        urlValue === "" &&
                        "Please enter a valid google search url to find occurrences of your keyword"
                    }
                    sx={{
                        width: "800px",
                        m: 3,
                    }}
                    value={urlValue}
                    onChange={(e) => {
                        setUrlValue(e.target.value)
                    }}
                />
                <TextField
                    id="outlined-basic"
                    label="Keyword"
                    variant="outlined"
                    onFocus={() => setkeywordFieldTouched(true)}
                    error={keywordFieldTouched && keywordValue === ""}
                    helperText={
                        keywordFieldTouched &&
                        keywordValue === "" &&
                        "Keyword can not be empty"
                    }
                    sx={{
                        width: "800px",
                        m: 3,
                    }}
                    value={keywordValue}
                    onChange={(e) => {
                        setKeywordValue(e.target.value)
                    }}
                />
                <Box
                    display={"flex"}
                    width={"300px"}
                    justifyContent={"space-evenly"}
                    mt={3}
                >
                    <Button
                        variant="contained"
                        sx={{
                            width: "125px",
                        }}
                        onClick={onSearch}
                    >
                        Search
                    </Button>
                    <Button
                        variant="outlined"
                        sx={{
                            width: "125px",
                        }}
                        onClick={onReset}
                    >
                        Reset
                    </Button>
                </Box>
            </Box>
            <Box display={"flex"} justifyContent={"center"}>
                {loading && <Typography py={3}>Loading...</Typography>}
                {resultVisible && !loading && (
                    <ScraperResultText
                        url={urlSearchValue}
                        keyword={keywordSearchValue}
                        result={occurrenceLocations}
                        matches={matches}
                        error={scrapeError}
                    />
                )}
            </Box>
        </Box>
    )
}
