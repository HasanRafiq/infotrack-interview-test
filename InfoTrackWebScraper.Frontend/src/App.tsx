import { Box, Typography } from "@mui/material"
import { WebScraperPanelComponent } from "./components/WebScraperPanel.tsx/WebScraperPanelComponent"

const App = () => {
    return (
        <Box
            display={"flex"}
            height={"100vh"}
            flexDirection={"column"}
            justifyContent={"center"}
            alignItems={"center"}
        >
            <Typography variant="h2" mb={6} fontWeight={600}>
                Welcome to the InfoTrack web scraper
            </Typography>
            <Typography variant="h4" mb={6}>
                Enter a google search url and keyword to find the occurrences of
                the keyword in the results
            </Typography>
            <WebScraperPanelComponent />
        </Box>
    )
}

export default App
